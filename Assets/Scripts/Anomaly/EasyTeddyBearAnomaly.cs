using UnityEngine;

public class EasyTeddyBearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalTeddyBear = storage.normalTeddyBear;
        GameObject anomalyTeddyBear = storage.anomalyTeddyBear;
        normalTeddyBear.SetActive(false);
        anomalyTeddyBear.SetActive(true);
    }
    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.EasyTeddyBear;
    }

}