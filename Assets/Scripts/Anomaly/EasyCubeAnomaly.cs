using UnityEngine;

public class EasyCubeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalCube = storage.normalCube;
        GameObject anomalyCube = storage.anomalyCube;
        normalCube.SetActive(false);
        anomalyCube.SetActive(true);
    }
    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyCube;
    }
}