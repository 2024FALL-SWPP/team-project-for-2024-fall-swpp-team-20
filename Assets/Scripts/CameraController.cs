using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    private float rotateY;
    private float currentX;
    public float cameraRotateSpeed => player.rotateSpeed;
    private InteractionHandler interactionHandler;

    private Control control;

    private void Start() {
        interactionHandler = FindObjectOfType<InteractionHandler>().GetComponent<InteractionHandler>();
    }

    private void OnEnable()
    {
        control = new Control();
        control.Enable();
        control.NewMap.Rotate.performed += OnRotate;
        control.NewMap.ObjectInteraction.performed += OnObjectInteractionPerformed;
    }
    private void OnDisable()
    {
        control.NewMap.Rotate.performed -= OnRotate;
        control.NewMap.ObjectInteraction.performed -= OnObjectInteractionPerformed;
        control.Disable();
    }
    public void Initialize() {
        rotateY = 0;
        transform.localRotation = Quaternion.identity;
    }

    public void OnObjectInteractionPerformed(InputAction.CallbackContext value)
    {
        interactionHandler.HandleInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstance().GetState() != GameState.Playing) return;
    }

    public void OnRotate(InputAction.CallbackContext value)
    {
        if (!player.canMove || GameManager.GetInstance().GetState() != GameState.Playing) return;
        Vector2 input = value.ReadValue<Vector2>();
        rotateY = input.y;

        currentX = transform.localEulerAngles.x;
        if (CanRotate(currentX, rotateY))
        {
            transform.Rotate(cameraRotateSpeed * rotateY * Time.deltaTime * Vector3.left, Space.Self);
        }
    }

    private bool CanRotate(float currentX, float rotateY)
    {
        if (currentX < 60 || currentX > 300) return true;
        if (rotateY < 0 && 200 < currentX) return true;
        if (rotateY > 0 && 160 > currentX) return true;
        return false;
    }
}
