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

    private GameState State => GameManager.instance.state;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Initialize() {
        mouseDeltaX = 0;
        transform.localRotation = Quaternion.identity;

        isJumping = false;
        canSleep = false;
        canMove = false;
    }
    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime; // Added variable deltaTime to avoid calling Time.deltaTime multiple times in Update()
        if (State != GameState.Playing) return;
        if (canMove)
        {
            transform.Rotate(deltaTime * rotateSpeed * mouseDeltaX * Vector3.up);
            transform.Translate(deltaTime * moveSpeed * new Vector3(moveDirection.x, 0, moveDirection.y), Space.Self);
        }
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
        if (State != GameState.Playing) return;
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
            canSleep = true;
            EnableSleepUI();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canSleep && other.gameObject.CompareTag("Bed"))
        {
            canSleep = true;
            EnableSleepUI();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            canSleep = false;
            DisableSleepUI();
        }
    }

    public void OnBedInteraction(InputValue value)
    {
        if (State != GameState.Playing) return;
        if (canSleep)
        {
            float input = value.Get<float>();
            GameManager.instance.pm.TryBedInteraction(input > 0);
        }
    }


    private void EnableSleepUI()
    {
        GameManager.instance.um.ShowSleepInfo();
    }


    private void DisableSleepUI()
    {
        GameManager.instance.um.HideInfo();
    }

    //Pause when press esc
    //Resume when press Esc again
    public void OnPause(InputValue value)
    {
        if (value.Get<float>() > 0) {
            if (State == GameState.Playing)
            {
                Time.timeScale = 0f;
                GameManager.instance.um.ShowStateUI(GameState.Pause);
                GameManager.instance.Pause();
            }
            else if (State == GameState.Pause) {
                Time.timeScale = 1f;
                GameManager.instance.um.HideStateUI();
                GameManager.instance.Play();
            }
        }

    }


    // Can restart only in pause, gameover, gamestart
    public void OnRestart(InputValue value) {
        if (value.Get<float>() > 0) {
            if (State != GameState.Playing) {
                GameManager.instance.pm.GameStart();
            }
        }
    }
}
