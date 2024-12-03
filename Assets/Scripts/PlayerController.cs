using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpawnPosition
{
    Original,
    Lava,
    Chessboard,
}
public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private Vector3 worldMoveDirection;

    private Rigidbody rb;

    public bool canMove = false;

    public float moveSpeed = 10f; // 원하는 속도로 증가
    public float rotateSpeed;
    public float mouseDeltaX;

    public float jumpForce;

    private bool isJumping;
    private bool isTouchingSide = false;

    public bool canSleep;
    private bool inBedRange;
    private HardAnomalyCode currentAnomaly;

    private Control control;

    [SerializeField] private SpawnPositions spawnPositions;
    //private GameState State => GameManager.instance.state;

    public void SetPlayerController(SpawnPosition positionCode)
    {
        SetTransform(positionCode);
        SetCameraClippingPlanes(positionCode);
        SetPhysical(positionCode);
    }

    public void SetTransform(SpawnPosition positionCode)
    {
        TransformSet targetTransform = null;

        switch (positionCode)
        {
            case SpawnPosition.Original:
                targetTransform = spawnPositions.originalSpawn;
                break;
            case SpawnPosition.Lava:
                targetTransform = spawnPositions.lavaSpawn;
                break;
            case SpawnPosition.Chessboard:
                targetTransform = spawnPositions.chessboardSpawn;
                break;
            default:
                break;
        }

        transform.position = targetTransform.localPosition;
        transform.rotation = Quaternion.Euler(targetTransform.eulerRotation);
        transform.localScale = targetTransform.scale;
    }

    public void SetPhysical(SpawnPosition positionCode)
    {
        switch (positionCode)
        {
            case SpawnPosition.Original:
            case SpawnPosition.Lava:
                moveSpeed = spawnPositions.origialMoveSpeed;
                jumpForce = spawnPositions.originalJumpForce;
                break;
            case SpawnPosition.Chessboard:
                moveSpeed = spawnPositions.chessMoveSpeed;
                jumpForce = spawnPositions.chessJumpForce;
                break;
            default:
                break;
        }
    }

    public void SetCameraClippingPlanes(SpawnPosition positionCode)
    {
        Camera camera = GetComponentInChildren<Camera>();
        float plane = positionCode switch
        {
            SpawnPosition.Original => 0.3f,
            SpawnPosition.Lava => 0.3f,
            SpawnPosition.Chessboard => 0.02f,
            _ => 0.3f
        };
        camera.nearClipPlane = plane;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        control = new Control();
        EnableInput();
    }

    private void OnDisable() => DisableInput();
    public void Initialize()
    {
        mouseDeltaX = 0;
        transform.localRotation = Quaternion.identity;

        isJumping = false;
        SetInBedRange(false);
        SetSleep(false);
        SetMove(false);
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (GameManager.GetInstance().GetState() == GameState.Playing && canMove)
        {
            if (moveDirection.magnitude > 0)
                worldMoveDirection = transform.TransformVector(new Vector3(moveDirection.x, 0f, moveDirection.y).normalized);
            else
                worldMoveDirection = Vector3.zero;

            Vector3 velocity = rb.velocity;
            Vector3 desiredVelocity = moveSpeed * worldMoveDirection;

            Vector3 velocityChange = new Vector3(desiredVelocity.x - velocity.x, 0f, desiredVelocity.z - velocity.z);

            // 공중에 있고 측면에 붙어 있을 때 수평 이동을 제한
            if (isJumping && isTouchingSide)
            {
                velocityChange = Vector3.zero;
            }

            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            transform.Rotate(0.01f * rotateSpeed * mouseDeltaX * Vector3.up);

            isJumping = !IsGrounded();
            Debug.Log(isTouchingSide);
        }
    }

    private bool IsGrounded()
    {
        float distanceToGround = 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (GameManager.GetInstance().GetState() == GameState.Playing && canMove)
        {
            moveDirection = value.ReadValue<Vector2>();
        }
    }

    public void OnMoveCanceled(InputAction.CallbackContext value)
    {
        moveDirection = Vector2.zero;
    }

    public void OnRotate(InputAction.CallbackContext value)
    {
        if (GameManager.GetInstance().GetState() == GameState.Playing && canMove)
        {
            Vector2 input = value.ReadValue<Vector2>();
            mouseDeltaX = input.x;

        }
    }

    public void OnRotateCanceled(InputAction.CallbackContext value)
    {
        mouseDeltaX = 0;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing) return;
        float jumped = value.ReadValue<float>();

        if (jumped > 0f && !isJumping)
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // 접촉 면의 법선 벡터의 Y 값이 일정 값 이하이면 측면 충돌로 간주
            if (Mathf.Abs(contact.normal.y) < 0.3f)
            {
                isTouchingSide = true;
                return; // 측면 충돌을 감지하면 더 이상 반복할 필요 없음
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // 접촉 면의 법선 벡터의 Y 값이 일정 값 이하이면 측면 충돌로 간주
            if (Mathf.Abs(contact.normal.y) < 0.3f)
            {
                isTouchingSide = true;
                return; // 측면 충돌을 감지하면 더 이상 반복할 필요 없음
            }
        }
        isTouchingSide = false; // 측면 충돌이 없으면 false로 설정
    }

    private void OnCollisionExit(Collision collision)
    {
        isTouchingSide = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            SetInBedRange(true);
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.ClearHard);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!inBedRange && other.gameObject.CompareTag("Bed"))
        {
            SetInBedRange(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            SetInBedRange(false);
        }
    }

    public void OnBedInteraction(InputAction.CallbackContext value)
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing) return;
        if (ActuallyCanSleep())
        {
            float input = value.ReadValue<float>();
            BedInteractionType type = input > 0 ? BedInteractionType.Sleep : BedInteractionType.Wakeup;
            GameManager.GetInstance().bedInteractionManager.TryBedInteraction(type);
        }
    }

    private void ToggleSleepUI()
    {
        if (ActuallyCanSleep()) GameManager.GetInstance().um.ShowSleepInfo();
        else GameManager.GetInstance().um.HideSleepInfo();
    }


    //Pause when press esc
    //Resume when press Esc again
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0)
        {
            if (GameManager.GetInstance().GetState() == GameState.Playing)
            {
                Time.timeScale = 0f;
                GameManager.GetInstance().um.ShowStateUI(GameState.Pause);
                GameManager.GetInstance().Pause();
            }
            else if (GameManager.GetInstance().GetState() == GameState.Pause)
            {
                Time.timeScale = 1f;
                GameManager.GetInstance().um.HideStateInfo();
                GameManager.GetInstance().Play();
            }
        }

    }


    // Can restart only in pause, gameover, gamestart
    public void OnRestart(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0)
        {
            GameManager.GetInstance().um.HideStateInfo();
            GameManager.GetInstance().stageManager.GameStart();
        }
    }

    public void OnQuitUIScript(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0 && GameManager.GetInstance().GetState() == GameState.ReadingScript)
        {
            GameManager.GetInstance().Play();
            GameManager.GetInstance().um.HideCharacterScript();
        }
    }



    public void EnableInput()
    {
        control.Enable();
        control.NewMap.Move.performed += OnMove;
        control.NewMap.Move.canceled += OnMoveCanceled;
        control.NewMap.Jump.performed += OnJump;
        control.NewMap.Pause.performed += OnPause;
        control.NewMap.BedInteraction.performed += OnBedInteraction;
        control.NewMap.Rotate.performed += OnRotate;
        control.NewMap.Rotate.canceled += OnRotateCanceled;
        control.NewMap.Restart.performed += OnRestart;
        control.NewMap.QuitUIScript.performed += OnQuitUIScript;

    }

    public void DisableInput()
    {
        control.NewMap.Move.performed -= OnMove;
        control.NewMap.Move.canceled -= OnMoveCanceled;
        control.NewMap.Jump.performed -= OnJump;
        control.NewMap.Pause.performed -= OnPause;
        control.NewMap.BedInteraction.performed -= OnBedInteraction;
        control.NewMap.Rotate.performed -= OnRotate;
        control.NewMap.Rotate.canceled -= OnRotateCanceled;
        control.NewMap.Restart.performed -= OnRestart;
        control.NewMap.QuitUIScript.performed -= OnQuitUIScript;

        control.Disable();
    }

    private bool ActuallyCanSleep()
    {
        return canSleep && inBedRange && (currentAnomaly == HardAnomalyCode.NotInHard);
    }

    public void SetSleep(bool available)
    {
        canSleep = available;
        ToggleSleepUI();
    }

    public void SetInBedRange(bool inRange)
    {
        inBedRange = inRange;
        ToggleSleepUI();
    }

    public void SetMove(bool available)
    {
        canMove = available;
    }

    public void SetAnomalyType(HardAnomalyCode code) => currentAnomaly = code;
}
