using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyCanvasFlipAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject Canvas = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("NormalCanvas").gameObject;
        Canvas.transform.rotation = Quaternion.Euler(0, -90, 0);
        Canvas.transform.localPosition = new Vector3(-7.547f, 1.713f, 3.52f);
    }
}
