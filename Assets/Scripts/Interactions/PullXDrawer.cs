using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullXDrawer : InteractableObject
{
    public Animator pull_01;
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
            StartCoroutine(OpenDrawer());
        }
        else
        {
            StartCoroutine(CloseDrawer());
        }
    }

    public override bool IsInteractable() => true;

    private IEnumerator OpenDrawer()
    {
        pull_01.Play("openpull_01");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    private IEnumerator CloseDrawer()
    {
        pull_01.Play("closepush_01");
        open = false;
        yield return new WaitForSeconds(.5f);
    }

}
