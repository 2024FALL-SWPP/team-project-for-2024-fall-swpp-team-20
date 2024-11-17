using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLavaAnomaly : HardAnomaly
{

    public override void Apply(GameObject map)
    {
        GameObject lava = map.transform.Find("Lavas").gameObject;
        laptop = map.transform.Find("Interior").Find("2nd Floor").
            Find("Apartment_01").Find("Props").Find("laptop").GetComponent<Laptop>();
        lava.SetActive(true);
        GameManager.GetInstance().um.ShowHealthImage();
        
    }

    public override void SetHardAnomalyCodeForLaptop()
    {
        Debug.Log("HELLO");
        laptop.SetAnomalyCode(2);
    }


}
