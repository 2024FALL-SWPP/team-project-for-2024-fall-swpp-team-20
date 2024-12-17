using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : InteractableObject
{
    public bool inAnomaly = false;
    private Animator ani;

    protected override void Start()
    {
        base.Start();
        ani = GetComponent<Animator>();
        ani.SetInteger("RotateState", 0);
    }

    public override void Interact(GameObject obj)
    {

        if (inAnomaly)
        {
            //Debug.Log("Anomaly");
            ani.SetInteger("RotateState", 1);
        } // 1: Infinitely rotate
        else
        {
            //Debug.Log("Normal");
            ani.SetInteger("RotateState", 2);
        } // 2: Temporarily rotate
    }

    //public override bool IsInteractable() => true;

}
