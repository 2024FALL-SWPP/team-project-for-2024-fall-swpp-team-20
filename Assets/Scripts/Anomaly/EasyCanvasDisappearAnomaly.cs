using UnityEngine;

public class EasyCanvasDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject giraffeCanvas = storage.giraffeCanvas;
        giraffeCanvas.SetActive(false);
    }
}