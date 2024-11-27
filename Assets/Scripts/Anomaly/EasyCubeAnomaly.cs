using UnityEngine;

public class EasyCubeAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject normalCube = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Furniture").Find("Table_Computer_01_Setup").Find("Drawer_L_01").Find("cube_shuffled").gameObject;
        GameObject anomalyCube = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Furniture").Find("Table_Computer_01_Setup").Find("Drawer_L_01").Find("cube").gameObject;
        normalCube.SetActive(false);
        anomalyCube.SetActive(true);
    }
}