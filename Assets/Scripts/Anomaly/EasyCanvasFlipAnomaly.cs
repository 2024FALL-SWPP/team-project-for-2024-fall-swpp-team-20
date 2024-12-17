using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyCanvasFlipAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject Canvas = storage.giraffeCanvas;
        Canvas.transform.rotation = Quaternion.Euler(0, -90, 180);
        Canvas.transform.localPosition = new Vector3(-7.547f, 1.713f, 3.52f);
    }
    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyCanvasFlip;
    }
}
