using UnityEngine;

public class EasyDresserBackOpenAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject dresser = map.transform.Find("Interior/2nd Floor/Apartment_01/Furniture/Dresser Closet_01").gameObject;
        GameObject backOpenedDresser = map.transform.Find("Interior/2nd Floor/Apartment_01/Furniture/Dresser Closet_01_BackOpened").gameObject;
        dresser.SetActive(false);
        backOpenedDresser.SetActive(true);
    }
}