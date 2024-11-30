using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HardAnomalyCode
{
    NotInHard,
    Lava,
    TimeBomb,
    Chess,
    Chessboard
}

public abstract class HardAnomaly : Anomaly
{
    public Laptop laptop;
    public abstract void SetHardAnomalyCode();

    public abstract HardAnomalyCode GetHardAnomalyCode();
}
