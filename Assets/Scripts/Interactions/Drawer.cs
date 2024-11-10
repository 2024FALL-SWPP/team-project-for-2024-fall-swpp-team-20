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
                StartCoroutine(OpenDrawer());
            }
            else
            {
                StartCoroutine(CloseDrawer());
            }
        }
    }
    public void Interact(GameObject obj)
    {
        Open = !Open;
    }

    public IEnumerator OpenDrawer()
    {
        Vector3 targetPosition = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.01f);
            yield return null;
        }
        transform.position = targetPosition;
    }

    public IEnumerator CloseDrawer()
    {
        Vector3 targetPosition = new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.01f);
            yield return null;
        }
        transform.position = targetPosition;
    }
}
