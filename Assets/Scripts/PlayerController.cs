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

    public bool canSleep;

    private void Start()
    {
        mouseDeltaX = 0;
        transform.localRotation = Quaternion.identity;
        rb = GetComponent<Rigidbody>();
        isJumping = false;
        canSleep = false;
        canMove = false;
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
        moveDirection = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        mouseDeltaX = input.x;
    }

    public void OnJump(InputValue value)
    {
        float jumped = value.Get<float>();

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
            Debug.Log("HELLO1");
            canSleep = true;
            EnableSleepUI();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canSleep && other.gameObject.CompareTag("Bed"))
        {
            Debug.Log("HELLO2");
            canSleep = true;
            EnableSleepUI();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            Debug.Log("HELLO3");
            canSleep = false;
            DisableSleepUI();
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


    private void EnableSleepUI() {
        GameManager.instance.um.ShowSleepInfo();
    }


    private void DisableSleepUI() {
        GameManager.instance.um.HideInfo();
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
