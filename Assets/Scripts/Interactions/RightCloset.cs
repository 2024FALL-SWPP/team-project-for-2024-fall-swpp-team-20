using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCloset : MonoBehaviour, IInteractable
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
                StartCoroutine(OpenDoor());
            }
            else
            {
                StartCoroutine(CloseDoor());
            }
        }
    }
    public void Interact(GameObject obj)
    {
        Open = !Open;
    }
    public IEnumerator OpenDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 0);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2f);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    public IEnumerator CloseDoor()
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 90);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 2f);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    public bool IsInteractable() => true;
}
