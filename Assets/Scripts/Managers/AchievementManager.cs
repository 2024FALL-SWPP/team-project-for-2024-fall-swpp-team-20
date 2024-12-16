using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        if (GameManager.GetInstance().am != null && GameManager.GetInstance().am != this) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }
    public void ShowClearPanel(int newAchievementFlag)
    {

    }

    // store completed achievement until current stage
    private int tempFlag;


    // store newly completed achievement in current stage.
    private int newAchievements;


    public void Initialize(bool start)
    {
        tempFlag = GameManager.GetInstance().GetAchievementFlag();
        if (tempFlag < 0) tempFlag = 0;
        ShowClearPanel(newAchievements);
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
        int flag = (int)Mathf.Pow(2, (int)achievement);

        if ((tempFlag & flag) == 0) {
            tempFlag += flag;
            newAchievements += flag;
            Debug.Log($"Achievement Clear: {achievement}");
        }
        else return;
        if (tempFlag % (int)Mathf.Pow(2, 24) == (int)Mathf.Pow(2, 24) - 1) ClearAchievement(Achievements.ClearAllAnomaly);
        if (tempFlag == (int)Mathf.Pow(2, 36) - 1) ClearAchievement(Achievements.AllAchievementClear);
    }
    public void ClearAchievement(AnomalyCode anomaly)
    {
        if (anomaly == AnomalyCode.Chessboard) anomaly = AnomalyCode.HardChess; // Convert Chessboard to HardChess
        Debug.Log($"Anomaly clear: {anomaly}, {(Achievements)(int)anomaly}");
        int flag = (int)Mathf.Pow(2, (int)anomaly);

        if ((tempFlag & flag) == 0)
        {
            tempFlag += flag;
            newAchievements += flag;
        }
    }

    public void SaveAchievementFlag()
    {
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
