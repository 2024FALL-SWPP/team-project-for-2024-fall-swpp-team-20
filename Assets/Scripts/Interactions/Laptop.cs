using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Laptop : MonoBehaviour, IInteractable
{
    public Material[] materialSet1;
    public Material[] materialSet2;
    public Material[] anomalyMaterialSet;
    public Renderer screen;

    private Camera mainCamera;
    private Camera laptopCamera;


    private bool reading = false;
    public TMP_Text laptopInfo;
    public TMP_Text quitInfo;

    public LaptopSystem laptopInput;


    private float[] waitingTimes = { 1f, 0.7f, 0.5f, 0.3f, 0.2f };

    // anomalyCode for laptop: 0 if no anomaly, 1 if laptop anomaly, 2 if hard Anomaly
    private int anomalyCode = 0;

    private bool power = false;

    private void Start()
    {

        DisableText();
        mainCamera = Camera.main;
        laptopCamera = GameObject.Find("LaptopCamera").GetComponent<Camera>();
        laptopCamera.gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        laptopInput = new LaptopSystem();
        laptopInput.Enable();
        laptopInput.Laptop.Quit.performed += StopReadingLaptop;
    }

    private void OnDisable()
    {
        laptopInput.Laptop.Quit.performed -= StopReadingLaptop;
        laptopInput.Disable();
    }
    public void Interact(GameObject obj)
    {

        switch (anomalyCode)
        {
            case 0: // no anomaly
                if (power) TurnOff();
                else TurnOn();
                break;
            case 1: // easy laptop anomaly
                power = !power;
                if (anomalyCode == 1 && power) StartCoroutine(ToggleLaptop1());
                break;
            case >= 2: // hard anomaly
                if (!power) TurnOn();
                else if (!reading) StartReadingLaptop();
                break;
            default:
                break;
        }
    }

    private void TurnOn()
    {
        power = true;
        screen.materials = materialSet1;
        if (anomalyCode >= 2) EnableText();
    }

    private void TurnOff()
    {
        power = false;
        screen.materials = materialSet2;
        if (anomalyCode >= 2) DisableText();
    }

    private IEnumerator ToggleLaptop1()
    {
        foreach (float time in waitingTimes)
        {
            screen.materials = anomalyMaterialSet;
            yield return new WaitForSeconds(time);
            screen.materials = materialSet2;
            yield return new WaitForSeconds(0.2f);
        }
        power = false;
    }

    public void SetAnomalyCode(int code)
    {
        anomalyCode = code;
        switch (code)
        {
            case 0:
            case 1:
                laptopInfo.text = "";
                break;
            case 2: // lava anomaly
                laptopInfo.text = "THE FLOOR IS LAVA!\nIt seems that you cannot endure much time with this lava.. Find any way to Go out!!";
                break;
            case 3: // Time bomb anomaly
                laptopInfo.text = "TIME BOMB!\nYou have to defuse the bomb before it explodes!";
                break;
        }
    }

    public bool IsInteractable()
    {
        if (anomalyCode == 1) return !power;
        else return true;
    }

    private void StartReadingLaptop()
    {
        reading = true;
        mainCamera.gameObject.SetActive(false);
        laptopCamera.gameObject.SetActive(true);
        GameManager.GetInstance().stageManager.ToggleActionAvailability(false);
        GameManager.GetInstance().um.TemporaryHideInteractionInfo();
    }

    private void StopReadingLaptop(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().state != GameState.Playing) return;
        laptopCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        GameManager.GetInstance().stageManager.ToggleActionAvailability(true);
        GameManager.GetInstance().um.ShowLaptopInteractionInfo();
        reading = false;
    }

    private void EnableText()
    {
        laptopInfo.enabled = true;
        quitInfo.enabled = true;
    }
    private void DisableText()
    {
        laptopInfo.enabled = false;
        quitInfo.enabled = false;
    }
}
