using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySpintopAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        Totem totem = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("inception_totem").GetComponent<Totem>();
        totem.inAnomaly = true;
    }
}
