using UnityEngine;

public class EasyDiceAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject anomalyDice = map.transform.Find("Bedroom").Find("anomalyDice").gameObject;
        GameObject normalDice = map.transform.Find("Bedroom").Find("normalDice").gameObject;
        normalDice.SetActive(false);
        anomalyDice.transform.position = normalDice.transform.position;
        anomalyDice.transform.rotation = normalDice.transform.rotation;
        anomalyDice.SetActive(true);
    }
}