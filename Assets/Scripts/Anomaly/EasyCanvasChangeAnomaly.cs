using UnityEngine;

public class EasyCanvasChangeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalCanvas = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("NormalCanvas").gameObject;
        GameObject anomalCanvas = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("AnomalCanvas").gameObject;
        normalCanvas.SetActive(false);
        anomalCanvas.SetActive(true);
    }
}