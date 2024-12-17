using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLavaAnomaly : HardAnomaly
{

    public override void Apply(GameObject map)
    {
        GameObject lava = map.transform.Find("Lavas").gameObject;
        laptop = storage.laptopObject.GetComponent<Laptop>();
        lava.SetActive(true);
        GameManager.GetInstance().sm.PlayLavaSound(lava);
        GameManager.GetInstance().um.ShowHealthImage();
        PlayerController pc = GameManager.GetInstance().player.GetComponent<PlayerController>();
        pc.SetPlayerController(SpawnPosition.Lava);
        RemoveOriginalObjects();
    }

    // Used some objects as platform to escape the room, removing same objects at the original position
    // it is better to just manipulate transform of objects, but it is too hard work..
    private void RemoveOriginalObjects()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("LavaPlatform");
        foreach (GameObject platform in platforms)
        {
            Object.Destroy(platform);
        }
    }
    public override AnomalyCode GetAnomalyCode()
    {
        return AnomalyCode.HardLava;
    }

}
