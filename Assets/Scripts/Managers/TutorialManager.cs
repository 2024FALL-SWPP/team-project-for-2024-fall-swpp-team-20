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
        interactionHandler = Camera.main.GetComponent<InteractionHandler>();
        yield return StartCoroutine(ShowMoveTutorial());
        yield return StartCoroutine(ShowJumpTutorial());
        yield return StartCoroutine(ShowObjectInteractionTutorial());
        uiManager.isBedInteractionTutorial = true;
        yield return StartCoroutine(ShowBedInteractionTutorial());
    }

    private IEnumerator ShowMoveTutorial()
    {
        uiManager.ShowTutorialText("Use WASD keys to move.");
        while (!playerController.HasMoved())
        {
            yield return null;
        }
        uiManager.ShowTutorialText("Move Tutorial Completed!");
        yield return new WaitForSeconds(2f);
        uiManager.HideTutorialText();
    }

    private IEnumerator ShowJumpTutorial()
    {
        uiManager.ShowTutorialText("Press Space to jump.");
        while (!playerController.HasJumped())
        {
            yield return null;
        }
        uiManager.ShowTutorialText("Jump Tutorial Completed!");
        yield return new WaitForSeconds(2f);
        uiManager.HideTutorialText();
    }

    private IEnumerator ShowObjectInteractionTutorial()
    {
        uiManager.ShowTutorialText("There are some object that you can interact. \nClick the mouse to interact with objects. \nYou can also open the door as the same way.");
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
        uiManager.ShowTutorialText("If you know whether this is a dream or not, go to bed.\n Press F to sleep, G to wake up to interact with the bed. \nFor now, press F to get sleep and start the game.");
        while (!playerController.HasInteractedWithBed())
        {
            yield return null;
        }
    }
}