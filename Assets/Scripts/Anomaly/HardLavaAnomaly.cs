using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLavaAnomaly : HardAnomaly
{
    GameObject lava;
    public override void Apply(GameObject map)
    {
        lava = map.transform.Find("Lavas").gameObject;
        lava.SetActive(true);
    }

    public override void GiveInformation()
    {
        Debug.Log("Showing Lava Information..");
        // Not Implemented yet
    }

    public override void InitializeForHard()
    {
        GameManager.GetInstance().um.ShowHealthImage();
    }
}
