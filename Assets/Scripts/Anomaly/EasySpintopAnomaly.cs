using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySpintopAnomaly : Anomaly
{
    public override void Apply(GameObject myBedroom)
    {
        Totem totem = FindObjectOfType<Totem>();
        totem.inAnomaly = true;
    }
}
