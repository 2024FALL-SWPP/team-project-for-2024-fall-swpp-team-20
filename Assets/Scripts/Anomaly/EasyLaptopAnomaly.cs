using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLaptopAnomaly : Anomaly
{
    public override void Apply(GameObject map) {
        Laptop laptop = FindObjectOfType<Laptop>();
        laptop.inAnomaly = true;
    }
}
