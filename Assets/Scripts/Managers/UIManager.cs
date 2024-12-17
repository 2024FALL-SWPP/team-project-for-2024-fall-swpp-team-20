using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private Transform canvasTransform;
    // for Main Scene
    // Something will be needed..

    // for Ingame scene

    private Text generalInfo;
    private Text interactionInfo;
    private GameObject sleepText;
    private GameObject pianoText;
    private Text stateInfo;
    private Text timerText;
    private InputField passwordField;
    private RectTransform health;
    private Image healthBar;
    private float maxHealthWidth;
    private Image cursorImage;
    private GameObject characterScriptPanel;
    private Text characterScript;
    private GameObject cover;
    [SerializeField] private TextMeshProUGUI tutorialText;
    public bool isBedInteractionTutorial;
    private GameObject settingPanel;

    private Control control;
    public void Initialize()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            canvasTransform = FindAnyObjectByType<Canvas>().transform;
            settingPanel = canvasTransform.Find("SettingBackground").gameObject;
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            canvasTransform = FindAnyObjectByType<Canvas>().transform;
            settingPanel = canvasTransform.Find("SettingBackground").gameObject;
            generalInfo = canvasTransform.Find("InformationText").GetComponent<Text>();
            interactionInfo = canvasTransform.Find("InteractionText").GetComponent<Text>();
            sleepText = canvasTransform.Find("SleepText").gameObject;
            pianoText = canvasTransform.Find("PianoText").gameObject;
            stateInfo = canvasTransform.Find("FinishText").GetComponent<Text>();
            cursorImage = canvasTransform.Find("Cursor").gameObject.GetComponent<Image>();
            health = canvasTransform.Find("Health").GetComponent<RectTransform>();
            healthBar = canvasTransform.Find("HealthBar").GetComponent<Image>();
            timerText = canvasTransform.Find("TimerText").GetComponent<Text>();
            passwordField = canvasTransform.Find("passwordField").GetComponent<InputField>();
            characterScriptPanel = canvasTransform.Find("CharacterScriptPanel").gameObject;
            characterScript = characterScriptPanel.GetComponentInChildren<Text>();
            cover = canvasTransform.Find("Cover").gameObject;
            tutorialText = canvasTransform.Find("TutorialText").GetComponent<TextMeshProUGUI>();
            isBedInteractionTutorial = true;
            HideEverything();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void ShowSleepInfo()
    {
        if (!isBedInteractionTutorial)
        {
            return;
        }
        // generalInfo.enabled = true;
        // generalInfo.text = "Press [F] to Sleep\n Press [G] to Wake Up";
        sleepText.gameObject.SetActive(true);
    }

    //Make cursor Red if Interaction is able with mouse click
    public void ShowInteractionInfo(GameObject target)
    {
        interactionInfo.enabled = true;
        interactionInfo.text = $"Mouse click to interact with {target.transform.name}";
        // cursorImage.color = Color.red;
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
        // interactionInfo.enabled = true;
        // interactionInfo.text = "Play the piano with the key 1~8 \n Press [Q] to Exit";
        interactionInfo.enabled = false;
        pianoText.gameObject.SetActive(true);
        HideCursor();
    }



    public void HideStateInfo() => stateInfo.enabled = false;

    // Make cursor White if Interaction becomes unable
    public void HideInteractionInfo()
    {
        interactionInfo.enabled = false;
        // cursorImage.color = Color.white;
    }

    public void HideSleepInfo()
    {
        generalInfo.enabled = false;
        sleepText.gameObject.SetActive(false);
    }

    public void HidePianoInteractionInfo()
    {
        pianoText.gameObject.SetActive(false);
        interactionInfo.enabled = true;
        interactionInfo.text = $"Mouse click to interact with piano";
        ShowCursor();
    }

    public void ShowHealthImage()
    {
        health.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
    }
    public void HideHealthImage()
    {
        health.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
    }

    public void SetHealthImage(float health)
    {
        if (maxHealthWidth == 0)
        {
            maxHealthWidth = this.health.sizeDelta.x;
        }

        // Health 값에 비례하여 가로 길이를 설정
        float newWidth = Mathf.Max(maxHealthWidth * (health / 100f), 0);
        this.health.sizeDelta = new Vector2(newWidth, this.health.sizeDelta.y);
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

    public void ShowCover() => cover.SetActive(true);
    public void HideCover() => cover.SetActive(false);
    public void ShowCharacterScript(AnomalyCode code)
    {
        characterScriptPanel.SetActive(true);
        characterScript.text = "";
        switch (code)
        {
            case AnomalyCode.HardLava:
                characterScript.text = "Lava is coming! Run!";
                break;
            case AnomalyCode.HardTimeBomb:
                characterScript.text = "Time Bomb is ticking!";
                break;
            case AnomalyCode.HardChess:
                characterScript.text = "Hmm.. I wanna play some chess game..";
                break;
            case AnomalyCode.Chessboard:
                characterScript.text = "Kill all the chess pieces!\nMouse click to shoot\nDon't get hit by them!";
                SetCharacterScriptPanelHeight(500f);
                break;
            case AnomalyCode.HardReverseMap:
                characterScript.text = "Map is reversed! You should go back to the bed!";
                break;
            case AnomalyCode.HardFruitDrop:
                characterScript.text = "Fruit is dropping! You might get hurt!";
                break;
            case AnomalyCode.HardVisibility:
                characterScript.text = "Why can't I see anything?! maybe I just need to go sleep again..";
                break;
            default:
                break;
        }
        Time.timeScale = 0;
    }

    private void SetCharacterScriptPanelHeight(float newHeight)
    {
        RectTransform panelRect = characterScriptPanel.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(panelRect.sizeDelta.x, newHeight);
    }

    public void HideCharacterScript()
    {
        characterScriptPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void HidePianoText()
    {
        pianoText.SetActive(false);
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
        HideCharacterScript();
        HideCover();
        HideTutorialText();
        HidePianoText();
        HideSettingPanel();
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
        cursorImage.gameObject.SetActive(false);
    }

    private void ShowCursor()
    {
        cursorImage.gameObject.SetActive(true);
    }

    public void ShowTutorialText(string text)
    {
        tutorialText.text = text;
        tutorialText.enabled = true;
    }

    public void HideTutorialText()
    {
        tutorialText.enabled = false;
    }

    public void ShowSettingPanel()
    {
        settingPanel.gameObject.SetActive(true);
    }


    public void HideSettingPanel()
    {
        settingPanel.gameObject.SetActive(false);
    }

    private void HideSettingPanelByInput(InputAction.CallbackContext value)
    {
        if (value.ReadValue<float>() > 0) HideSettingPanel();

    }

    private void OnEnable()
    {
        EnableInputInMainScene();
    }

    private void OnDisable()
    {
        DisableInputInMainScene();
    }
    public void EnableInputInMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            control = new Control();
            control.Enable();
            control.NewMap.Pause.performed += HideSettingPanelByInput;
        }
    }

    public void DisableInputInMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            control.NewMap.Pause.performed -= HideSettingPanelByInput;
            control.Disable();
        }
    }
}
