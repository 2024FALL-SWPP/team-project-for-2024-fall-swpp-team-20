using UnityEngine;

public class EasyCubeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject normalCube = storage.normalCube;
        GameObject anomalyCube = storage.anomalyCube;
        normalCube.SetActive(false);
        anomalyCube.SetActive(true);
    }
}