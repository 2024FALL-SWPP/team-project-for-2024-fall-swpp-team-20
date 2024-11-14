using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    //private float rotateX;
    private float rotateY;
    private float currentX;
    public float cameraRotateSpeed => player.rotateSpeed;

    public bool CanMove => player.canMove;
    public bool canInteract;

    // true when player can interact with anything right now using mouse click
    private bool immInteractable;

    private int layerMask;
    private RaycastHit hit;

    private GameState State => GameManager.instance.state;

    private Control control;
    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Interactable");
    }

    private void OnEnable()
    {
        control = new Control();
        control.Enable();
        control.NewMap.Rotate.performed += OnRotate;
    }
    private void OnDisable()
    {
        control.NewMap.Rotate.performed -= OnRotate;
        control.Disable();
    }
    public void Initialize() {
        //rotateX = 0;
        rotateY = 0;
        transform.localRotation = Quaternion.identity;
        immInteractable = false;
        canInteract = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (State != GameState.Playing) return;
        //transform.RotateAround(transform.position, Vector3.up, rotateX);
        
        if (canInteract && Physics.Raycast(transform.position, transform.forward, out hit, 5f, layerMask))
        {
            if (!immInteractable)
            {
                ShowInteractableUI(hit);
            }
        }
        else if (immInteractable) HideInteractableUI();
        Debug.DrawRay(transform.position, 5 * transform.forward, Color.red);
    }

    private void ShowInteractableUI(RaycastHit hit) {
        immInteractable = true;
        GameManager.instance.um.ShowInteractionInfo(hit);
    }

    private void HideInteractableUI() {
        immInteractable = false;
        GameManager.instance.um.HideInteractionInfo();
    }
    public void OnRotate(InputAction.CallbackContext value)
    {
        if (State != GameState.Playing) return;
        Vector2 input = value.ReadValue<Vector2>();
        rotateY = CanMove ? input.y : 0;

        currentX = transform.localEulerAngles.x;
        if (CanRotate(currentX, rotateY))
        {
            transform.Rotate(cameraRotateSpeed * rotateY * Time.deltaTime * Vector3.left, Space.Self);
        }
    }

    public void OnObjectInteraction(InputValue value) {
        /*if (CanInteract && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.distance < 5f && value.Get<float>() > 0f)
            {
                GameObject target = hit.transform.gameObject;
                target.GetComponent<IInteractable>().Interact(target);
            }
        }*/
        if (State != GameState.Playing) return;
        if (immInteractable) {
            GameObject target = hit.transform.gameObject;
            target.GetComponent<IInteractable>().Interact(target);
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
