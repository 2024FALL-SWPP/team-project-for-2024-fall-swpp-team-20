using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Only works for Lava Anomaly
public class LaptopPlace : MonoBehaviour, IInteractable
{
    private bool active = false;
    public void Interact(GameObject obj) { 
        
    }

    public bool IsInteractable() => active;

    public void Activate() => active = true;
    public void Deactivate() => active = false;
}
