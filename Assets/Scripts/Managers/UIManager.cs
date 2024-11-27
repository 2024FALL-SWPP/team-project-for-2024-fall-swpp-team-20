using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Transform canvasTransform;
    // for Main Scene
    // Something will be needed..

    // for Ingame scene

    private Text generalInfo;
    private Text interactionInfo;
    private Text stateInfo;
    private Text timerText;
    private InputField passwordField;
    private RectTransform health;
    private RawImage[] cursorImage;
    private GameObject characterScriptPanel;
    private Text characterScript;

    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            canvasTransform = FindAnyObjectByType<Canvas>().transform;
            generalInfo = canvasTransform.Find("InformationText").GetComponent<Text>();
            interactionInfo = canvasTransform.Find("InteractionText").GetComponent<Text>();
            stateInfo = canvasTransform.Find("FinishText").GetComponent<Text>();
            cursorImage = canvasTransform.Find("Cursor").gameObject.GetComponentsInChildren<RawImage>();
            health = canvasTransform.Find("Health").GetComponent<RectTransform>();
            timerText = canvasTransform.Find("TimerText").GetComponent<Text>();
            passwordField = canvasTransform.Find("passwordField").GetComponent<InputField>();
            characterScriptPanel = canvasTransform.Find("CharacterScriptPanel").gameObject;
            characterScript = characterScriptPanel.GetComponentInChildren<Text>();
        }
        HideEverything();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowSleepInfo()
    {
        generalInfo.enabled = true;
        generalInfo.text = "Press [F] to Sleep\n Press [G] to Wake Up";
    }

    //Make cursor Red if Interaction is able with mouse click
    public void ShowInteractionInfo(RaycastHit hit)
    {
        interactionInfo.enabled = true;
        interactionInfo.text = $"Mouse click to interact with {hit.transform.name}";
        foreach (RawImage i in cursorImage)
        {
            i.color = Color.red;
        }
    }

    public void ShowStateUI(GameState state)
    {
        stateInfo.enabled = true;
        switch (state)
        {
            case GameState.GameClear:
                stateInfo.text = "Game Clear!";
                break;
            case GameState.GameOver:
                stateInfo.text = "Game Over...";
                break;
            case GameState.Pause:
                stateInfo.text = "Pause..";
                break;
            default:
                Debug.LogError($"Unexpected State: {state}");
                break;
        }
    }

    public void ShowPianoInteractionInfo()
    {
        interactionInfo.enabled = true;
        interactionInfo.text = "Play the piano with the key 1~8 \n Press [Q] to Exit";
        HideCursor();
    }



    public void HideStateInfo() => stateInfo.enabled = false;

    // Make cursor White if Interaction becomes unable
    public void HideInteractionInfo()
    {
        interactionInfo.enabled = false;
        foreach (RawImage i in cursorImage)
        {
            i.color = Color.white;
        }
    }

    public void HideSleepInfo()
    {
        generalInfo.enabled = false;
    }

    public void HidePianoInteractionInfo()
    {
        interactionInfo.enabled = true;
        interactionInfo.text = $"Mouse click to interact with piano";
        ShowCursor();
    }

    public void ShowHealthImage() => health.gameObject.SetActive(true);
    public void HideHealthImage() => health.gameObject.SetActive(false);
    public void SetHealthImage(float health)
    {
        this.health.sizeDelta = new Vector2(Mathf.Max(6 * health, 0), 50f);
    }

    public void ShowTimerImage() => timerText.enabled = true;
    public void HideTimerImage() => timerText.enabled = false;
    public void SetTimerText(int min, int sec) => timerText.text = $"{min:D2}:{sec:D2}";

    public void ShowPasswordInputField() => passwordField.gameObject.SetActive(true);
    public void HidePasswordInputField() => passwordField.gameObject.SetActive(false);
    public string GetPassword() => passwordField.text;
    public void ShowPasswordCompareResult(string passwordInput, string realPassword)
    {
        RawImage[] trialImages = new RawImage[4];
        trialImages[0] = GameObject.Find("Result").transform.Find("RawImage").GetComponent<RawImage>();
        trialImages[1] = GameObject.Find("Result").transform.Find("RawImage (1)").GetComponent<RawImage>();
        trialImages[2] = GameObject.Find("Result").transform.Find("RawImage (2)").GetComponent<RawImage>();
        trialImages[3] = GameObject.Find("Result").transform.Find("RawImage (3)").GetComponent<RawImage>();
        for (int i = 0; i < 4; i++)
        {
            //implement numBall logic : if correct, show Green. if the number is on other position, show yellow. else red
            if (passwordInput[i] == realPassword[i])
            {
                trialImages[i].color = Color.green;
            }
            else if (realPassword.Contains(passwordInput[i]))
            {
                trialImages[i].color = Color.yellow;
            }
            else
            {
                trialImages[i].color = Color.red;
            }
        }
    }

    public void UpdateTrialCount(int trialCount)
    {
        Text trialText = GameObject.Find("TrialText").GetComponent<Text>();
        trialText.text = $"Trial: {trialCount}";
    }

    public void SetHardAnomalyInfo(HardAnomalyCode code) {
        switch (code) {
            case HardAnomalyCode.Lava:
                break;
            case HardAnomalyCode.TimeBomb:
                break;
            default:
                break;
        }
    }

    //Reset UI when new stage starts
    public void HideEverything()
    {
        HideSleepInfo();
        HideInteractionInfo();
        HideStateInfo();
        HideHealthImage();
        HideTimerImage();
        HidePasswordInputField();
    }

    // For Watching Laptop
    public void TemporaryHideInteractionInfo()
    {
        interactionInfo.text = $"";
        HideCursor();
    }
    public void ShowLaptopInteractionInfo()
    {
        interactionInfo.text = $"Mouse click to interact with laptop";
        ShowCursor();
    }

    private void HideCursor()
    {
        foreach (RawImage i in cursorImage)
        {
            i.enabled = false;
        }
    }

    private void ShowCursor()
    {
        foreach (RawImage i in cursorImage)
        {
            i.enabled = true;
        }
    }
}
