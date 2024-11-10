using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLaptopAnomaly : Anomaly
{
    public override void Apply(GameObject map) {
        Laptop laptop = map.transform.Find("Bedroom").Find("laptop").GetComponent<Laptop>();
        laptop.inAnomaly = true;
    }
}
