using UnityEngine;

public class EasyBookDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject anomalyBook = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("book (1)").gameObject;
        anomalyBook.SetActive(false);
    }
}