using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardVisibilityAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        GameManager.GetInstance().um.ShowHealthImage();
        GameManager.GetInstance().um.ShowCover();
        PlayerController pc = GameManager.GetInstance().player.GetComponent<PlayerController>();
        PlayerInformation pi = GameManager.GetInstance().player.GetComponent<PlayerInformation>();
        InteractableObject[] list = GameObject.FindObjectsByType<InteractableObject>(FindObjectsSortMode.None);
        foreach (InteractableObject obj in list) { 
            obj.inVisibilityAnomaly = true;
        }
        storage.door.GetComponent<OpenCloseDoor>().OpenRemotely();
        pc.SetPlayerController(SpawnPosition.Visibility);
        pi.HurtPlayerByDOT(0.5f);
    }
    public override HardAnomalyCode GetHardAnomalyCode()
    {
        return HardAnomalyCode.Visibility;
    }
}
