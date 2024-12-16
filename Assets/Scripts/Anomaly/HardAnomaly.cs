using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class HardAnomaly : Anomaly
{
    public Laptop laptop;

    public virtual void SetAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(GetAnomalyCode());
    }
}
