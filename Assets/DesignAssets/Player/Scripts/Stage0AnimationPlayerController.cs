using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Stage0AnimationPlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    private Rigidbody rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    public TextMeshProUGUI sleepText;
    public Vector3 sleepPoint = Vector3.zero;
    public float proximityThreshold = 2f;

    private Control control;
    private bool isDisabled = false; // 이동 및 회전을 비활성화할지 여부

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (sleepText != null) sleepText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        control = new Control();
        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame) // F 키 입력 체크
        {
            isDisabled = !isDisabled; // 상태 토글
        }

        if (isDisabled)
        {
            moveDirection = Vector3.zero; // 이동 중지
            animator.SetBool("IsWalking", false);
            return; // 이동 및 회전 중지
        }

        Vector3 movement = transform.forward * moveDirection.z + transform.right * moveDirection.x;
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        animator.SetBool("IsWalking", moveDirection.magnitude > 0);

        float distanceToSleepPoint = Vector3.Distance(transform.position, sleepPoint);
        if (distanceToSleepPoint <= proximityThreshold)
        {
            if (sleepText != null) sleepText.gameObject.SetActive(true);
        }
        else
        {
            if (sleepText != null) sleepText.gameObject.SetActive(false);
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        if (isDisabled) return; // 이동 비활성화 중에는 입력 무시
        Vector2 input = value.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0, input.y);
    }

    public void OnRotate(InputAction.CallbackContext value)
    {
        if (isDisabled) return; // 회전 비활성화 중에는 입력 무시
        Vector2 input = value.ReadValue<Vector2>();
        float mouseDeltaX = input.x;
        transform.Rotate(Vector3.up, mouseDeltaX * rotateSpeed * Time.deltaTime);
    }

    private void EnableInput()
    {
        control.Enable();
        control.NewMap.Move.performed += OnMove;
        control.NewMap.Move.canceled += ctx => moveDirection = Vector3.zero;
        control.NewMap.Rotate.performed += OnRotate;
    }

    private void DisableInput()
    {
        control.NewMap.Move.performed -= OnMove;
        control.NewMap.Move.canceled -= ctx => moveDirection = Vector3.zero;
        control.NewMap.Rotate.performed -= OnRotate;
        control.Disable();
    }
}
