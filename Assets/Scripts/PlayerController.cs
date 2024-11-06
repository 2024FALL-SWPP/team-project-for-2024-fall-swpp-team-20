using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;

    private Rigidbody rb;

    public bool canMove = false;

    public float moveSpeed;
    public float rotateSpeed;
    public float mouseDeltaX;

    public float jumpForce;
    private float deltaTime;

    private bool isJumping;

    public bool canSleep = false;

    private void Start()
    {
        mouseDeltaX = 0;
        transform.localRotation = Quaternion.identity;
        rb = GetComponent<Rigidbody>();
        isJumping = false;
    }
    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime; // Added variable deltaTime to avoid calling Time.deltaTime multiple times in Update()
        transform.Rotate(deltaTime * rotateSpeed * mouseDeltaX * Vector3.up);
        transform.Translate(deltaTime * moveSpeed * new Vector3(moveDirection.x, 0, moveDirection.y), Space.Self);
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("Move..");
        moveDirection = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        Debug.Log("Rotate..");
        Vector2 input = value.Get<Vector2>();
        mouseDeltaX = input.x;
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Jump..");
        float jumped = value.Get<float>();
        Debug.Log(jumped);

        if (jumped > 0f && !isJumping)
        {
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
        if (other.gameObject.CompareTag("Bed"))
        {
            canSleep = true;
            // Show UI to inform that you can sleep
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canSleep && other.gameObject.CompareTag("Bed"))
        {
            canSleep = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
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

    public void ToggleInteraction(bool canInteract)
    {
        canSleep = canInteract;
        canMove = canInteract;
    }

    /* README: Added temporary pause button for only testing
     * It works only in Editor
     */
    public void OnPause(InputValue value)
    {
#if UNITY_EDITOR
        if (value.Get<float>() > 0f)
        {
            Debug.Break();
        }
#endif
    }
}
