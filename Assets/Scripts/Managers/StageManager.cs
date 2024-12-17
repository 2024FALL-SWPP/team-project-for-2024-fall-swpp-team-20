using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class StageManager : MonoBehaviour
{
    private static int currentStage;

    public int GetCurrentStage() => currentStage;
    public void SetCurrentStage(int stage) => currentStage = stage;
    private bool haveAnomaly;
    public bool GetHaveAnomaly() => haveAnomaly;

    private AnomalyCode currentAnomaly;
    public AnomalyCode GetCurrentAnomaly() => currentAnomaly;
    public void SetAnomalyType(AnomalyCode code) => currentAnomaly = code;
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
        AnomalyCode code = mc.GenerateMap(haveAnomaly, stage);

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

        SetAnomalyType(code);
        // Set time
        ToggleActionAvailability(true);
        interactionHandler.SetMouseClickAction(0);
        landscapeManager.ChangeLandscape(stage);

        Cursor.lockState = CursorLockMode.Locked;

        if (!Anomaly.AnomalyIsHard(code))
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
        AchievementManager am = GameManager.GetInstance().am;
        am.ClearAchievement(Achievements.GameClear);
        if (!am.Missed()) am.ClearAchievement(Achievements.GameClearWithoutMiss);
        GameManager.GetInstance().Clear();
        SceneManager.LoadScene("GameClearScene");
    }

    public void GameOver()
    {
        // Game over logic
        GameManager.GetInstance().GameOver();
        SceneManager.LoadScene("GameOverScene");
    }
    public void HandleSleepOutcome(BedInteractionType type)
    {
        GameManager.GetInstance().am.SaveAchievementFlag();
        bool isHard = Anomaly.AnomalyIsHard(pc.GetAnomalyType());
        Debug.Log(isHard);
        bool sleep = type == BedInteractionType.Sleep;
        if (isHard)
        {
            if (type == BedInteractionType.ClearHard)
            {
                Succeed();
                SceneManager.LoadScene("AnomalyTrueWakeUpScene");
            }
            else
            {
                Fail();
                SceneManager.LoadScene("AnomalyFalseWakeUpScene");
            }
        }
        else if (sleep && haveAnomaly)
        {
            Fail();
            SceneManager.LoadScene("SleepScene");
        }
        else if (!sleep && !haveAnomaly)
        {
            Fail();
            SceneManager.LoadScene("WakeupFalseScene");
        }
        else if (sleep && !haveAnomaly)
        {
            Succeed();
            SceneManager.LoadScene("SleepScene");
        }
        else if (!sleep && haveAnomaly)
        {
            Succeed();
            SceneManager.LoadScene("WakeupTrueScene");
        }
    }

    private void Succeed()
    {
        ++currentStage;
        AchievementManager am = GameManager.GetInstance().am;
        am.ClearAchievement(currentAnomaly);
        if (currentAnomaly == AnomalyCode.HardLava) { 
            if (am.TimeLeft()) am.ClearAchievement(Achievements.LavaAnomalyClearFast);
            if (!am.DamageTaken()) am.ClearAchievement(Achievements.LavaAnomalyNoDamage);
        }
        if (currentAnomaly == AnomalyCode.HardVisibility && am.TimeLeft()) am.ClearAchievement(Achievements.VisibilityAnomalyClearFast);
        if (currentAnomaly == AnomalyCode.HardChess) {
            if (!am.DamageTaken()) am.ClearAchievement(Achievements.ChessAnomalyNoDamage);
            if (am.GetShootCount() >= 200) am.ClearAchievement(Achievements.ChessAnomalyMachineGun);
        }
        if (currentAnomaly == AnomalyCode.HardFruitDrop && !am.DamageTaken()) am.ClearAchievement(Achievements.FruitAnomalyNoDamage);
    }

    private void Fail()
    {
        // TODO: Animation
        if (currentStage > 1) currentStage--;
        AchievementManager am = GameManager.GetInstance().am;
        if (Anomaly.AnomalyIsHard(currentAnomaly)) am.ClearAchievement(Achievements.MissHardAnomaly);
        else am.ClearAchievement(Achievements.MissEasyAnomaly);
        am.SetMissFlag();
    }

    public void QuitGame() {
        GameManager.GetInstance().ResetGame();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.GetInstance().ResetGame();
        //TODO: save achievement state
    }
}
