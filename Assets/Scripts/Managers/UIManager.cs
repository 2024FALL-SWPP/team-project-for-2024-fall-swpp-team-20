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
    private RawImage[] cursorImage;

    private void Start()
    {
        canvasTransform = FindAnyObjectByType<Canvas>().transform;

        if (SceneManager.GetActiveScene().name == "GameScene") {
            generalInfo = canvasTransform.Find("InformationText").GetComponent<Text>();
            interactionInfo = canvasTransform.Find("InteractionText").GetComponent<Text>();
            cursorImage = canvasTransform.Find("Cursor").gameObject.GetComponentsInChildren<RawImage>();
        }
        generalInfo.enabled = false;
        interactionInfo.enabled = false;
    }

    public void ShowSleepInfo() {
        generalInfo.enabled = true;
        generalInfo.text = "Press [F] to Sleep\n Press [G] to Wake Up";
    }

    //Make cursor Red if Interaction is able with mouse click
    public void ShowInteractionInfo(RaycastHit hit) {
        interactionInfo.enabled = true;
        interactionInfo.text = $"Mouse click to interact with {hit.transform.name}";
        foreach (RawImage i in cursorImage) {
            i.color = Color.red;
        }
    }


    // Make cursor White if Interaction becomes unable
    public void HideInteractionInfo() {
        interactionInfo.enabled = false;
        foreach (RawImage i in cursorImage) {
            i.color = Color.white;
        }
    }

    public void HideInfo() {
        generalInfo.enabled = false;
    }
}
