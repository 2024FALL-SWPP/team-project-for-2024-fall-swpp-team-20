using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float moveDirection;
    private float rotateDirection;
    private Rigidbody rb;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;

    private bool isJumping;

    public bool canSleep;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJumping = false;

        canSleep = true;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateDirection * rotateSpeed * Vector3.up);
        transform.Translate(moveSpeed * moveDirection * Vector3.forward, Space.Self);
    }

    public void OnMove(InputValue value) {
        float input = value.Get<float>();
        moveDirection = input;
        Debug.Log("trying to move");
    }

    public void OnRotate(InputValue value) { 
        float input = value.Get<float>();
        rotateDirection = input;
    }
    public void OnJump(InputValue value) {

        float jumped = value.Get<float>();
        Debug.Log(jumped);

        if (jumped > 0f && !isJumping) {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;

        //TODO: isJumping = false when only collision with plane
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bed")) {
            canSleep = true;
            // Show UI to inform that you can sleep
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed")) {
            canSleep = false;
            // Show UI to inform that you cannot sleep
        }
    }

    public void OnBedInteraction(InputValue value)
    {
        if (canSleep)
        {
            float input = value.Get<float>();
            GameManager.instance.pm.TryBedInteraction(input > 0);
        }
    }
}
