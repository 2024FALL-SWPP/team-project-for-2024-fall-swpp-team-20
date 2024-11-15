using UnityEngine;

public class EasyChairLegDisappearAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject DiningTable = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Furniture").Find("DiningTable").gameObject;
        for (int i = 0; i < 4; i++)
        {
            GameObject ChairLeg = DiningTable.transform.Find("Chair " + i).Find("Legs Chair").gameObject;
            ChairLeg.SetActive(false);
        }
    }
}