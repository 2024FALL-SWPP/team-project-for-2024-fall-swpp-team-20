using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    //private float rotateX;
    private float rotateY;
    public float cameraRotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //rotateX = 0;
        rotateY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(transform.position, Vector3.up, rotateX);
        transform.Rotate(Vector3.left * Time.deltaTime * rotateY * cameraRotateSpeed, Space.Self);
        if (transform.localRotation.y > 90f) transform.localRotation = Quaternion.Euler(90f * Vector3.up);
        else if (transform.localRotation.y < -90f) transform.localRotation = Quaternion.Euler(-90f * Vector3.up);
    }

    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        //rotateX = input.x;
        rotateY = input.y;
    }
}
