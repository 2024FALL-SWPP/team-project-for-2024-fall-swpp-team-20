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

    // Start is called before the first frame update
    void Start()
    {
        //rotateX = 0;
        rotateY = 0;
        transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, rotateX);
        currentX = transform.localEulerAngles.x;
        Debug.Log(currentX);
        if (canRotate(currentX, rotateY))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotateY * cameraRotateSpeed, Space.Self);
        }
    }

    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        //rotateX = input.x;
        rotateY = CanMove ? input.y : 0;
        Debug.Log(rotateY);
    }

    private bool canRotate(float currentX, float rotateY)
    {
        if (currentX < 60 || currentX > 300) return true;
        if (rotateY < 0 && 200 < currentX) return true;
        if (rotateY > 0 && 160 > currentX) return true;
        return false;
    }
}
