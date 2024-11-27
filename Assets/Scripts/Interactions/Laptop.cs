using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class Laptop : MonoBehaviour, IInteractable
{
    public Material[] materialSet1;
    public Material[] materialSet2;
    public Material[] anomalyMaterialSet;
    public Renderer screen;




    private float[] waitingTimes = { 1f, 0.7f, 0.5f, 0.3f, 0.2f };

    // anomalyCode for laptop: 0 if no anomaly, 1 if laptop anomaly
    private bool inAnomaly = false;

    private bool power = false;

    public void Interact(GameObject obj)
    {
        if (inAnomaly)
        {
            power = !power;
            if (power) StartCoroutine(ToggleLaptop1());
        }
        else {
            if (power) TurnOff();
            else TurnOn();
        }
        
    }

    private void TurnOn()
    {
        power = true;
        screen.materials = materialSet1;
    }

    private void TurnOff()
    {
        power = false;
        screen.materials = materialSet2;
    }

    private IEnumerator ToggleLaptop1()
    {
        foreach (float time in waitingTimes)
        {
            screen.materials = anomalyMaterialSet;
            yield return new WaitForSeconds(time);
            screen.materials = materialSet2;
            yield return new WaitForSeconds(0.2f);
        }
        power = false;
    }

    public void SetAnomaly() => inAnomaly = true;

    public bool IsInteractable()
    {
        if (inAnomaly) return !power;
        else return true;
    }


}
