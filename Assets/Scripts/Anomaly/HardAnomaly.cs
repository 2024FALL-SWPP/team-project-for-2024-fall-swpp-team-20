using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HardAnomalyCode
{
    NotInHard,
    Lava,
    TimeBomb,
    ReverseMap,
    FruitDrop,
    Chess,
    Chessboard,
    Visibility
}

public abstract class HardAnomaly : Anomaly
{
    public Laptop laptop;
    public void SetHardAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(GetHardAnomalyCode());
    }

    public abstract HardAnomalyCode GetHardAnomalyCode();
}
