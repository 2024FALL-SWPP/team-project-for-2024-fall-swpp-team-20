using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float rotateX;
    private float rotateY;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        rotateX = 0;
        rotateY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.RotateAround(transform.position, Vector3.up, rotateX);
        transform.RotateAround(transform.position, transform.right, -rotateY);
    }

    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        rotateX = input.x;
        rotateY = input.y;
    }
     // linting test
}
