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
        GameManager.GetInstance().player.transform.localScale = 0.05f * Vector3.one;
        RemoveOriginalObjects();
    }

    public override void SetHardAnomalyCodeForLaptop()
    {
        laptop.SetAnomalyCode(2);
    }

    // Used some objects as platform to escape the room, removing same objects at the original position
    // it is better to just manipulate transform of objects, but it is too hard work..
    private void RemoveOriginalObjects() {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("LavaPlatform");
        foreach (GameObject platform in platforms) {
            Object.Destroy(platform);
        }
    }


}
