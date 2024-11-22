using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Transform canvasTransform;
    // for Main Scene
    // Something will be needed..

    // for Ingame scene

    private Text generalInfo;
    private Text interactionInfo;
    private Text stateInfo;
    private RectTransform health;
    private RawImage[] cursorImage;


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

    //Reset UI when new stage starts
    public void HideEverything()
    {
        HideSleepInfo();
        HideInteractionInfo();
        HideStateInfo();
        HideHealthImage();
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
