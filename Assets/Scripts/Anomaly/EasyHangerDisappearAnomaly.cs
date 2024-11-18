using UnityEngine;

public class EasyHangerDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject Hanger = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("CoatHanger" + i).gameObject;
            Hanger.SetActive(false);
        }
    }
}