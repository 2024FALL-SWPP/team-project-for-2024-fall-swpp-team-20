using UnityEngine;

public class EasyCanvasChangeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalCanvas = storage.normalCanvas;
        GameObject anomalCanvas = storage.anomalCanvas;
        normalCanvas.SetActive(false);
        anomalCanvas.SetActive(true);
    }

    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyCanvasChange;
    }
}