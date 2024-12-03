using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour, IInteractable
{
    public bool inAnomaly = false;
    private Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetInteger("RotateState", 0);
    }
    public void Interact(GameObject obj)
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

    public bool IsInteractable() => true;

}
