using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCloset : MonoBehaviour, IInteractable
{
    private bool open = false;
    private bool Open {
        get => open;
        set {
            open = value;
            if(open)
            {
                transform.rotation = Quaternion.Euler(-90, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(-90, 0, 90);
            }
        }
    }
    public void Interact(GameObject obj) {
        Open = !Open;
    }
}
