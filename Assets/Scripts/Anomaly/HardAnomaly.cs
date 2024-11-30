using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HardAnomalyCode
{
    Lava,
    TimeBomb,
    Chess
}

public abstract class HardAnomaly : Anomaly
{
    public Laptop laptop;
    public abstract void SetHardAnomalyCode();
}
