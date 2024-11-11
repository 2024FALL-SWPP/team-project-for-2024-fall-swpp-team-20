using UnityEngine;

public class EasyBookDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject anomalyBook = map.transform.Find("Bedroom").Find("book (1)").gameObject;
        anomalyBook.SetActive(false);
    }
}