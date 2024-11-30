using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SpawnPosition { 
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

    public float moveSpeed;
    public float rotateSpeed;
    public float mouseDeltaX;

    public float jumpForce;

    private bool isJumping;


    public bool canSleep;
    private bool inBedRange;
    private bool inHardAnomaly;

    private Control control;

    [SerializeField] private SpawnPositions spawnPositions;
    //private GameState State => GameManager.instance.state;

    public void SetTransform(SpawnPosition positionCode) {
        TransformSet targetTransform = null;

        switch (positionCode) { 
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

    public void SetPhysical(SpawnPosition positionCode) {
        switch (positionCode) {
            case SpawnPosition.Original:
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

    public void SetCameraClippingPlanes(float plain) {
        Camera camera = GetComponentInChildren<Camera>();
        camera.nearClipPlane = plain;
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

            if (moveDirection.magnitude > 0) worldMoveDirection = transform.TransformVector(new Vector3(moveDirection.x, 0f, moveDirection.y));
            else worldMoveDirection = Vector3.zero;
            //transform.Translate(0.01f * moveSpeed * new Vector3(moveDirection.x, 0, moveDirection.y), Space.Self);
            rb.velocity = moveSpeed * worldMoveDirection + Vector3.up * rb.velocity.y;
            transform.Rotate(0.01f * rotateSpeed * mouseDeltaX * Vector3.up);
        }
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
        //TODO: isJumping = false when only collision with plane
        isJumping = false;
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
        if (value.ReadValue<float>() > 0 && GameManager.GetInstance().GetState() == GameState.Playing)
        {
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
        return canSleep && inBedRange && !inHardAnomaly;
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
    public void SetAnomalyType(bool hard) => inHardAnomaly = hard;
}
