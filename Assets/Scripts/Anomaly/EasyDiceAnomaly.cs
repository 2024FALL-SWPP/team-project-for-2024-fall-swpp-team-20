using UnityEngine;

public class EasyDiceAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject anomalyDice = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("anomalyDice").gameObject;
        GameObject normalDice = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("normalDice").gameObject;
        normalDice.SetActive(false);
        anomalyDice.transform.position = normalDice.transform.position;
        anomalyDice.transform.rotation = normalDice.transform.rotation;
        anomalyDice.SetActive(true);
    }
}