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
    GameFail, // Game over

    ClearAllAnomaly, // find and clear all anomalies
    LongGame, // meet 15 stage in one game

    ChessAnomalyClearWithNoDamage, // Clear Chess Anomaly without taking any damage
    LavaAnomalyClearFast, // Clear Lava Anomaly within 20 seconds
    VisibilityAnomalyClearFast, // Clear Visibility Anomaly within 20 seconds
}

public class AchievementManager : MonoBehaviour
{
    public void ShowClearPanel(Achievements achievement)
    {

    }

    // store achievement state. Does not change 
    private int tempFlag;

    public void Initialize()
    {
        tempFlag = GameManager.GetInstance().GetAchievementFlag();
        if (tempFlag < 0) tempFlag = 0;
    }

    public void ClearAchievement(Achievements achievement)
    {
        int flag = (int)Mathf.Pow(2, (int)achievement);

        if ((tempFlag & flag) == 0) tempFlag += (int)Mathf.Pow(2, (int)achievement);
    }

    public void SaveAchievementFlag()
    {
        GameManager.GetInstance().SetAchievementFlag(tempFlag);
    }
}
