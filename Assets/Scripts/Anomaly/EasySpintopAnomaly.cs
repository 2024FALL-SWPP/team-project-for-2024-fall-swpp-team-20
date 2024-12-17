using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySpintopAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        Totem totem = storage.spintopObject.GetComponent<Totem>();
        totem.inAnomaly = true;
    }

    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasySpintop;
    }
}
