using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    public Material[] materialSet1;
    public Material[] materialSet2;
    public Renderer screen;

    private bool power = false;
    private bool Power {
        get => power;
        set {
            Debug.Log(screen.materials[2]);
            power = value;
            screen.materials = value ? materialSet1 : materialSet2;
        }
    }
    public void Interact(GameObject obj) {
        Power = !Power;
    }
}
