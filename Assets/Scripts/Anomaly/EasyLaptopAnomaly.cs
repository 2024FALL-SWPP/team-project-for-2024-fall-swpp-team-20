using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyLaptopAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        Laptop laptop = storage.laptopObject.GetComponent<Laptop>();
        laptop.SetAnomaly();
    }
}
