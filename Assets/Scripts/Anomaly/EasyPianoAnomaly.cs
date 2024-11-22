using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPianoAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        Piano piano = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Furniture").Find("piano").GetComponent<Piano>();
        piano.inAnomaly = true;
    }
}
