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
    public bool CanInteract => player.canInteract;

    private int layerMask;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        //rotateX = 0;
        rotateY = 0;
        transform.localRotation = Quaternion.identity;
        layerMask = 1 << LayerMask.NameToLayer("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, rotateX);
        currentX = transform.localEulerAngles.x;
        if (CanRotate(currentX, rotateY))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotateY * cameraRotateSpeed, Space.Self);
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask)) {
            if (hit.distance < 10f) {
                Debug.Log("HIT!!");
            }
        }
        Debug.DrawRay(transform.position, 10 * transform.forward, Color.red);
    }

    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        //rotateX = input.x;
        rotateY = CanMove ? input.y : 0;
    }

    public void OnObjectInteraction(InputValue value) {
        if (CanInteract && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.distance < 10f && value.Get<float>() > 0f)
            {
                GameObject target = hit.transform.gameObject;
                target.GetComponent<IInteractable>().Interact(target);
            }
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
