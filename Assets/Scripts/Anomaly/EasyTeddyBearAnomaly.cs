using UnityEngine;

public class EasyTeddyBearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject normalTeddyBear = storage.normalTeddyBear;
        GameObject anomalyTeddyBear = storage.anomalyTeddyBear;
        normalTeddyBear.SetActive(false);
        anomalyTeddyBear.SetActive(true);
    }
}