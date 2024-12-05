using UnityEngine;

public class EasyCanvasChangeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject normalCanvas = storage.normalCanvas;
        GameObject anomalCanvas = storage.anomalCanvas;
        normalCanvas.SetActive(false);
        anomalCanvas.SetActive(true);
    }
}