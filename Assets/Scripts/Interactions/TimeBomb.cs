using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TimeBomb : InteractableObject
{
    public Camera mainCamera;
    private bool isInteracting = false;
    private string password;
    private int trialCount;
    private RawImage[] trialImages;

    public DigitalClockSystem digitalClockInput;

    protected override void Start()
    {
        trialCount = 5;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        base.Start();
        digitalClockInput = new DigitalClockSystem();
        digitalClockInput.Enable();
        digitalClockInput.DigitalClock.Insert.performed += OnInsert;
        digitalClockInput.DigitalClock.Quit.performed += EndInteraction;
    }

    private void OnDisable()
    {
        digitalClockInput.DigitalClock.Insert.performed -= OnInsert;
        digitalClockInput.DigitalClock.Quit.performed -= EndInteraction;
        digitalClockInput.Disable();
    }

    public override void Interact(GameObject obj)
    {
        StartInteraction();
    }

    private void StartInteraction()
    {
        isInteracting = true;
        GameManager.GetInstance().stageManager.ToggleActionAvailability(false);
        GameManager.GetInstance().um.ShowPasswordInputField();
        GameManager.GetInstance().um.UpdateTrialCount(trialCount);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        trialCount = 5;
        password = GeneratePassword();
        Debug.Log("password : " + password);
    }

    private string GeneratePassword()
    {
        string password = "";
        for (int i = 0; i < 4; i++)
        {
            int num = Random.Range(0, 10);
            if (!password.Contains(num.ToString()))
            {
                password += num.ToString();
            }
            else
            {
                i--;
            }
        }
        return password;
    }

    private void EndInteraction(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().state != GameState.Playing) return;
        if (!isInteracting) return;
        isInteracting = false;
        GameManager.GetInstance().stageManager.ToggleActionAvailability(true);
        GameManager.GetInstance().um.HidePasswordInputField();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnInsert(InputAction.CallbackContext context)
    {
        if (!isInteracting) return;
        if (GameManager.GetInstance().state != GameState.Playing) return;
        string passwordInput = GameManager.GetInstance().um.GetPassword();
        if (passwordInput.Length != 4)
        {
            return;
        }
        else if (passwordInput == password)
        {
            GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.ClearHard);
            return;
        }
        else
        {
            trialCount--;
            if (trialCount <= 0)
            {
                GameManager.GetInstance().bedInteractionManager.TryBedInteraction(BedInteractionType.Sleep);
                return;
            }
            GameManager.GetInstance().um.ShowPasswordCompareResult(passwordInput, password);
            GameManager.GetInstance().um.UpdateTrialCount(trialCount);
        }
    }

    public override bool IsInteractable() => true;
}
