using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uiManager;
    private InteractionHandler interactionHandler;

    [SerializeField] private TextMeshProUGUI tutorialText;

    public void StartTutorial()
    {
        StartCoroutine(RunTutorial());
    }

    private IEnumerator RunTutorial()
    {
        playerController = GameManager.GetInstance().player.GetComponent<PlayerController>();
        uiManager = GameManager.GetInstance().um;
        uiManager.isBedInteractionTutorial = false;
        interactionHandler = Camera.main.GetComponent<InteractionHandler>();
        yield return StartCoroutine(ShowIntroTutorial());
        yield return StartCoroutine(ShowObjectInteractionTutorial());
        uiManager.isBedInteractionTutorial = true;
        yield return StartCoroutine(ShowBedInteractionTutorial());
    }

    private IEnumerator ShowIntroTutorial()
    {
        uiManager.ShowTutorialText("From the next stage, you will enter a dream.\nFind any anomalies to escape the dream.");
        yield return new WaitForSeconds(5f);
        uiManager.ShowTutorialText("If you find an anomaly and wake up, time will rewind by 1 hour.\nConversely, if you find an anomaly and fall asleep, time will advance by 1 hour.");
        yield return new WaitForSeconds(5f);
        uiManager.ShowTutorialText("You can interact with objects by clicking the mouse.\nYou can also open doors in the same way.");
        yield return new WaitForSeconds(5f);
        uiManager.HideTutorialText();
    }

    private IEnumerator ShowObjectInteractionTutorial()
    {
        uiManager.ShowTutorialText("There are some objects that you can interact with.\nClick the mouse to interact with objects.\nYou can also open doors in the same way.");
        while (!interactionHandler.HasInteractedWithObject())
        {
            yield return null;
        }
        uiManager.ShowTutorialText("Object Interaction Tutorial Completed!");
        yield return new WaitForSeconds(2f);
        uiManager.HideTutorialText();
    }

    private IEnumerator ShowBedInteractionTutorial()
    {
        uiManager.ShowTutorialText("This is not a dream, so this room is normal. \nYou can proceed to the next stage by sleeping or waking up.");
        yield return new WaitForSeconds(5f);
        uiManager.ShowTutorialText("Before starting the game, please check the room and objects carefully.");
        yield return new WaitForSeconds(5f);
        uiManager.ShowTutorialText("When you're ready, go to the bed.\nPress F to sleep or G to wake up to interact with the bed.\nFor now, press F to sleep and start the game.");
        while (!playerController.HasInteractedWithBed())
        {
            yield return null;
        }
    }
}