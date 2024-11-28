using UnityEngine;

public class EasyDrawerMissingContentAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject drawer = map.transform.Find("Interior/2nd Floor/Apartment_01/Furniture/Table_Computer_01_Setup").gameObject;
        GameObject missingContent = drawer.transform.Find("Drawer_R_01/tissue").gameObject;
        missingContent.SetActive(false);
    }
}