using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLaptopAnomaly : Anomaly
{
    public override void Apply(GameObject map) {
        Laptop laptop = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("laptop").GetComponent<Laptop>();
        laptop.SetAnomalyCode(1);
    }
}
