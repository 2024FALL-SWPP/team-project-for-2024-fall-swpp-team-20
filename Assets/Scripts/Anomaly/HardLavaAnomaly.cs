using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLavaAnomaly : HardAnomaly
{

    public override void Apply(GameObject map)
    {
        GameObject lava = map.transform.Find("Lavas").gameObject;
        Laptop laptop = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("laptop").GetComponent<Laptop>();
        lava.SetActive(true);
        laptop.SetAnomalyCode(2);
        GameManager.GetInstance().um.ShowHealthImage();
        
    }

    public override void GiveInformation()
    {
        Debug.Log("Showing Lava Information..");
        // Not Implemented yet
    }


}
