using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public enum Achievements
{
    // Anomaly Clear
    EasyBookColorAnomalyClear,
    EasyBusHandleAnomalyClear,
    EasyCanvasChangeAnomalyClear,
    EasyCanvasDisappearAnomalyClear,
    EasyCanvasFlipAnomalyClear,
    EasyCubeAnomalyClear,
    EasyDiceAnomalyClear,
    EasyDigitalClockAnomalyClear,
    EasyDrawerMissingContentAnomalyClear,
    EasyDresserBackOpenAnomalyClear,
    EasyHangerDisaeppearAnomalyClear,
    EasyLaptopAnomalyClear,
    EasyLightAnomalyClear,
    EasyPianoAnomalyClear,
    EasyPlayerAnomalyClear,
    EasySofaDisasppearAnomalyClear,
    EasySpintopAnomalyClear,
    EasyTeddyBearAnomalyClear,
    HardChessAnomalyClear,
    HardFruitDropAnomalyClear,
    HardLavaAnomalyClear,
    HardReverseMapAnomalyClear,
    HardTimeBombAnomalyClear,
    HardVisibilityAnomalyClear,

    GameClear, // Game Clear
    GameClearWithoutMiss, // Game Cleared without any incorrect decision

    ClearAllAnomaly, // find and clear all anomalies
    MissEasyAnomaly, // make mistake in a easy anomaly for the first time
    MissHardAnomaly, // fail to escape a hard anomaly for the first time
    LongGame, // meet 15 stage in one game

    ChessAnomalyNoDamage, // Clear Chess Anomaly without taking any damage
    ChessAnomalyMachineGun, // Shoot 200 times in one chess anomaly
    LavaAnomalyClearFast, // Clear Lava Anomaly within 20 seconds
    LavaAnomalyNoDamage, // Clear Lava Anomaly without touching the lava
    VisibilityAnomalyClearFast, // Clear Visibility Anomaly within 20 seconds
    FruitAnomalyNoDamage, // Clear Fruit Anomaly without taking damage

    AllAchievementClear, // Clear All Achievements
}

public class AchievementManager : MonoBehaviour
{
    // variables for achievement
    float time;
    bool damageTaken;
    int stageCount;
    int shootCount;
    bool missFlag;

    public List<GameObject> achievementPanels;
    public Text achievementText;

    // store completed achievement until current stage
    private long tempFlag;

    // store newly completed achievement in current stage.
    private long newAchievements;

    public static string[] achievementNames = new string[] {
        "Book Color Anomaly",
        "Bus Handle Anomaly",
        "Canvas Change Anomaly",
        "Canvas Disappear Anomaly",
        "Canvas Flip Anomaly",
        "Cube Anomaly",
        "Dice Anomaly",
        "Digital Clock Anomaly",
        "Drawer Empty Anomaly",
        "Dresser Opened Anomaly",
        "Hanger Missing Anomaly",
        "Laptop Anomaly",
        "Light Anomaly",
        "Piano Anomaly",
        "Player Anomaly",
        "Sofa Missing Anomaly",
        "Spintop Anomaly",
        "Teddy Bear Anomaly",
        "Chess Master",
        "Cloudy With A Chance Of Fruit",
        "Floor is Lava!",
        "Upside Down",
        "Tik Tok Tik Tok..",
        "Overcome The Fear",
        "Escaped Dream!",
        "Perfect Game!",
        "Anomaly Master",
        "Bad Eye",
        "Bad Control",
        "Endless Dream",
        "Magnus Carlsen",
        "Machine Gun",
        "Hot Runner",
        "Hot Jumper",
        "Eyes are Not Necessary",
        "Nothing More Than Food",
        "Achievement Master"
    };

    public static string[] achievementConditions = new string[] {
        "Clear this Anomaly",
        "Clear game for the first time",
        "Clear game without wrong decision",
        "Find and clear all anomalies",
        "Fail to pass an easy anomaly",
        "Fail to pass a hard anomaly",
        "Play 15 stages in one game",
        "Clear chess game without taking damage",
        "Shoot more than 200 times in a chess game",
        "Clear lava anoamly within 20 seconds",
        "Clear lava anomaly without touching lava",
        "Clear visibility anomaly within 20 seconds",
        "Clear Fruit Anomaly without taking damage",
        "Clear ALL achievements and become the GOAT"
    };

    private void Awake()
    {
        if (GameManager.GetInstance().am != null && GameManager.GetInstance().am != this) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    public void ShowClearPanel()
    {
        achievementPanels = GameObject.FindGameObjectsWithTag("AchievementPanel").ToList();
        achievementText = GameObject.FindGameObjectWithTag("AchievementText").GetComponent<Text>();
        Debug.Log($"NewAchievementFlag: {newAchievements}");
        int panelIndex = 0;
        for (int i = 0; i < 36; i++)
        {
            if (((long)Mathf.Pow(2, i) & newAchievements) != 0)
            {
                if (panelIndex < 3)
                {
                    Text achievementName = achievementPanels[panelIndex].transform.GetChild(1).GetComponent<Text>();
                    achievementName.text = achievementNames[i].ToString();
                    achievementPanels[panelIndex].GetComponent<Animator>().SetTrigger("Move");
                }
                panelIndex++;
            }
        }
        if (panelIndex >= 3)
        {
            achievementText.text = $"And {panelIndex - 2} more..";
            achievementText.GetComponent<Animator>().SetTrigger("Move");
        }
        SaveAchievementFlag();
    }

    public void Initialize(bool start)
    {
        ShowClearPanel();
        tempFlag = GameManager.GetInstance().GetAchievementFlag();
        if (tempFlag < 0) tempFlag = 0;

        newAchievements = 0;
        if (start)
        {
            stageCount = 1;
            missFlag = false;
        }
        else
        {
            stageCount++;
            if (stageCount == 15) ClearAchievement(Achievements.LongGame);
        }
        shootCount = 0;
        time = 20f;

    }

    public void ClearAchievement(Achievements achievement)
    {
        long flag = (long)Mathf.Pow(2, (int)achievement);

        if ((tempFlag & flag) == 0)
        {
            tempFlag += flag;
            newAchievements += flag;
            Debug.Log($"Achievement Clear: {achievement}");
        }
        else return;
        if (tempFlag % (long)Mathf.Pow(2, 24) == (long)Mathf.Pow(2, (int)Achievements.ClearAllAnomaly) - 1) ClearAchievement(Achievements.ClearAllAnomaly);
        if (tempFlag == (long)Mathf.Pow(2, (int)Achievements.AllAchievementClear) - 1) ClearAchievement(Achievements.AllAchievementClear);
    }
    public void ClearAchievement(AnomalyCode anomaly)
    {
        if (anomaly == AnomalyCode.Chessboard) anomaly = AnomalyCode.HardChess; // Convert Chessboard to HardChess
        Debug.Log($"Anomaly clear: {anomaly}, {(Achievements)(int)anomaly}");
        long flag = (long)Mathf.Pow(2, (int)anomaly);

        if ((tempFlag & flag) == 0)
        {
            Debug.Log($"cleared flag: {flag}");
            tempFlag += flag;
            newAchievements += flag;
        }
    }

    public void SaveAchievementFlag()
    {
        Debug.Log($"Achievement Saved. flag: {tempFlag}");
        GameManager.GetInstance().SetAchievementFlag(tempFlag);
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameScene" && time > 0f)
            time -= Time.deltaTime;
    }

    public void SetMissFlag() => missFlag = true;
    public void SetDamageFlag() => damageTaken = true;

    public bool Missed() => missFlag;
    public bool DamageTaken() => damageTaken;

    public bool TimeLeft() => time > 0f;

    public void Shoot() => shootCount++;
    public int GetShootCount() => shootCount;

}
