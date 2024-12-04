using UnityEngine;

public class EasySofaDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject sofa = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Furniture").Find("Sofa").gameObject;
        sofa.SetActive(false);
    }
}