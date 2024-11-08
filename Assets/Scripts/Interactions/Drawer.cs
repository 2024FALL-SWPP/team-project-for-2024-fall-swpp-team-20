using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    private bool open = false;
    private bool Open
    {
        get => open;
        set
        {
            open = value;
            if (open)
            {
                transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
            }
        }
    }
    public void Interact(GameObject obj)
    {
        Open = !Open;
    }
}
