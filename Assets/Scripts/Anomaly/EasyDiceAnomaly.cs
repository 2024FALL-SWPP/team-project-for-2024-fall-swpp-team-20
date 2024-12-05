using UnityEngine;

public class EasyDiceAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        GameObject anomalyDice = storage.anomalyDice;
        GameObject normalDice = storage.normalDice;
        normalDice.SetActive(false);
        anomalyDice.transform.position = normalDice.transform.position;
        anomalyDice.transform.rotation = normalDice.transform.rotation;
        anomalyDice.SetActive(true);
    }
}