using UnityEngine;

public class EasyTeddyBearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalTeddyBear = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("teddybear").gameObject;
        GameObject anomalyTeddyBear = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("teddybear_anomaly").gameObject;
        normalTeddyBear.SetActive(false);
        anomalyTeddyBear.SetActive(true);
    }
}