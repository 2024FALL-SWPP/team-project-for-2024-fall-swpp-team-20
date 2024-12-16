using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    GameClear,
    GameClearWithoutMiss, // Game Cleared without any incorrect decision
    GameOver, // Game over

    ClearAllAnomaly, // find and clear all anomalies
    MissEasyAnomaly, // make mistake in a easy anomaly for the first time
    MissHardAnomaly, // fail to escape a hard anomaly for the first time
    LongGame, // meet 15 stage in one game

    ChessAnomalyNoDamage, // Clear Chess Anomaly without taking any damage
    ChessAnomalyMachineGun, // Shoot 200 times in one chess anomaly
    LavaAnomalyClearFast, // Clear Lava Anomaly within 20 seconds
    LavaAnomalyNoDamage, // Clear Lava Anomaly without touching the lava
    VisibilityAnomalyClearFast, // Clear Visibility Anomaly within 20 seconds

    AllAchievementClear, // Clear All Achievements
}

public class AchievementManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void ShowClearPanel(int newAchievementFlag)
    {

    }

    // store completed achievement for each stage. Reset to 0 every stage.
    private int tempFlag;

    private int newAchievements;


    public void Initialize()
    {
        tempFlag = GameManager.GetInstance().GetAchievementFlag();
        if (tempFlag < 0) tempFlag = 0;
        ShowClearPanel(newAchievements);
        newAchievements = 0;
    }

    public void ClearAchievement(Achievements achievement)
    {
        int flag = (int)Mathf.Pow(2, (int)achievement);

        if ((tempFlag & flag) == 0) {
            tempFlag += flag;
            newAchievements += flag;
        }
    }

    public void SaveAchievementFlag()
    {
        GameManager.GetInstance().SetAchievementFlag(tempFlag);
    }

}
