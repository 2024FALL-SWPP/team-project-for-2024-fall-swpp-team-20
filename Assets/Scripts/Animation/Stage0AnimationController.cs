using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Stage0AnimationController : MonoBehaviour
{
    private Animator animator;
    private InputAction interactAction;

    public Vector3 fixedPlayerPosition = new Vector3(-20, -0.2f, -5);
    public Vector3 fixedPlayerRotation = new Vector3(0, 90, 0);
    public Vector3 fixedCameraPosition = new Vector3(-17, 2, -2);
    public Vector3 fixedCameraRotation = new Vector3(20, 190, 0);

    private bool isInteracting = false;
    public Camera mainCamera;

    public RectTransform eyesUp;
    public RectTransform eyesDown;
    public float eyeCloseDuration = 3f;
    public Vector2 eyesUpTargetPosition = new Vector2(0, 100);
    public Vector2 eyesDownTargetPosition = new Vector2(0, -100);

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        interactAction = new InputAction("Interact", InputActionType.Button, "<Keyboard>/f");
        interactAction.performed += OnInteract;
        interactAction.Enable();
    }

    private void OnDestroy()
    {
        interactAction.Disable();
        interactAction.performed -= OnInteract;
    }

    private void Update()
    {
        if (isInteracting)
        {
            transform.position = fixedPlayerPosition;
            transform.rotation = Quaternion.Euler(fixedPlayerRotation);

            if (mainCamera != null)
            {
                mainCamera.transform.position = fixedCameraPosition;
                mainCamera.transform.rotation = Quaternion.Euler(fixedCameraRotation);
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && !isInteracting)
        {
            isInteracting = true;
            StartCoroutine(ActivateSleepTriggerAfterDelay(0f));
        }
    }

    private IEnumerator ActivateSleepTriggerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("SleepTrigger");
        StartCoroutine(CloseEyes());
    }

    private IEnumerator CloseEyes()
    {
        yield return new WaitForSeconds(5f);

        Vector2 eyesUpStart = eyesUp.anchoredPosition;
        Vector2 eyesDownStart = eyesDown.anchoredPosition;

        float elapsed = 0f;

        while (elapsed < eyeCloseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / eyeCloseDuration;

            eyesUp.anchoredPosition = Vector2.Lerp(eyesUpStart, eyesUpTargetPosition, t);
            eyesDown.anchoredPosition = Vector2.Lerp(eyesDownStart, eyesDownTargetPosition, t);

            yield return null;
        }

        eyesUp.anchoredPosition = eyesUpTargetPosition;
        eyesDown.anchoredPosition = eyesDownTargetPosition;

        SceneManager.LoadScene("GameScene");
    }
}
