using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class StageManager : MonoBehaviour
{
    private static int currentStage;
    public int GetCurrentStage() => currentStage;
    private bool haveAnomaly;
    public bool GetHaveAnomaly() => haveAnomaly;
    private GameObject player => GameManager.GetInstance().player;
    private MapController mc;
    private PlayerController pc;
    private InteractionHandler interactionHandler;
    private TutorialManager tutorialManager;

    private PlayerInformation pi;
    private LandscapeManager landscapeManager;
    private AnomalyManager am;
    private bool Test => am.test;
    public void InitializeVariables()
    {
        pc = player.GetComponent<PlayerController>();
        interactionHandler = FindObjectOfType<InteractionHandler>().GetComponent<InteractionHandler>();
        mc = FindObjectOfType<MapController>().GetComponent<MapController>();
        pi = player.GetComponent<PlayerInformation>();
        landscapeManager = FindObjectOfType<LandscapeManager>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        am = FindObjectOfType<AnomalyManager>();
    }

    public void GameStart(bool start = true)
    {
        pc.Initialize();
        if (start)
        {
            currentStage = 0;
            am.FillAnomaly();
            am.initializeAnomalyIndex();
        }
        InitializeStage(currentStage);
    }

    public void InitializeStage(int stage)
    {

        currentStage = stage;

        // 50% chance of having anomaly
        if (!Test && (stage == 0 || stage == 7 || Random.Range(0f, 1f) > 0.5) && stage != 5 && stage != 6)
            haveAnomaly = false;
        else
            haveAnomaly = true;

        // Reset UI
        GameManager.GetInstance().um.HideEverything();
        // Reset Player position, scale and Information
        pc.SetPlayerController(SpawnPosition.Original);
        pi.Initialize();

        if (!Test && haveAnomaly && am.noAnomalyCheck(stage))
        {
            GameOver();
            return;
        }

        // Create new stage map and inform player about it is hard anomaly or not
        HardAnomalyCode hard = mc.GenerateMap(haveAnomaly, stage);

        if (stage == 7)
        {
            GameClear();
            return;
        }

        if (stage == 0 && !Test)
        {
            tutorialManager.gameObject.SetActive(true);
            tutorialManager.StartTutorial();
        }
        else if (stage != 0)
        {
            tutorialManager.gameObject.SetActive(false);
        }

        pc.SetAnomalyType(hard);
        // Set time
        ToggleActionAvailability(true);
        interactionHandler.SetMouseClickAction(0);
        landscapeManager.ChangeLandscape(stage);

        Cursor.lockState = CursorLockMode.Locked;

        if (hard == HardAnomalyCode.NotInHard)
        {
            GameManager.GetInstance().sm.PlayEasyStageSound();
            GameManager.GetInstance().Play();
        }
        else
        {
            GameManager.GetInstance().sm.PlayHardStageSound();
            GameManager.GetInstance().ReadScript();
        }
    }

    public void ToggleActionAvailability(bool available)
    {
        pc.SetSleep(available);
        pc.SetMove(available);
        interactionHandler.SetInteract(available);
    }
    private void GameClear()
    {
        // Game clear logic
        GameManager.GetInstance().Clear();
        GameManager.GetInstance().um.ShowStateUI(GameState.GameClear);
    }

    public void GameOver()
    {
        // Game over logic
        GameManager.GetInstance().GameOver();
        GameManager.GetInstance().um.ShowStateUI(GameState.GameOver);
    }
    public void HandleSleepOutcome(BedInteractionType type)
    {
        bool isHard = pc.GetAnomalyType() != HardAnomalyCode.NotInHard;
        Debug.Log(isHard);
        bool sleep = type == BedInteractionType.Sleep;
        if (isHard)
        {
            if (type == BedInteractionType.ClearHard)
            {
                SceneManager.LoadScene("AnomalyTrueWakeUpScene");
                Succeed();
            }
            else
            {
                SceneManager.LoadScene("AnomalyFalseWakeUpScene");
                Fail();
            }
        }
        else if (sleep && haveAnomaly)
        {
            SceneManager.LoadScene("SleepScene");
            Fail();
        }
        else if (!sleep && !haveAnomaly)
        {
            SceneManager.LoadScene("WakeupFalseScene");
            Fail();
        }
        else if (sleep && !haveAnomaly)
        {
            SceneManager.LoadScene("SleepScene");
            Succeed();
        }
        else if (!sleep && haveAnomaly)
        {
            SceneManager.LoadScene("WakeupTrueScene");
            Succeed();
        }
    }

    private void Succeed()
    {
        ++currentStage;
    }

    private void Fail()
    {
        // TODO: Animation
        if (currentStage > 1) currentStage--;
    }
}
