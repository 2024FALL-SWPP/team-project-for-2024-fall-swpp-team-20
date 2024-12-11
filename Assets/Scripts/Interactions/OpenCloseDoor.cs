using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoor : InteractableObject
{
    public Animator openandclose;
    public bool open;

    protected override void Start()
    {
        base.Start();
        open = false;
    }

    public override void Interact(GameObject obj)
    {
        if (open == false)
        {
            StartCoroutine(opening());
        }
        else
        {
            StartCoroutine(closing());
        }
    }

    public override bool IsInteractable() => true;

    private IEnumerator opening()
    {
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator closing()
    {
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}
