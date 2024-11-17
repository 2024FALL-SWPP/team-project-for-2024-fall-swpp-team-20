using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    public Material[] materialSet1;
    public Material[] materialSet2;
    public Material[] anomalyMaterialSet;
    public Renderer screen;
    public Animator screenAni;
    public LaptopPlace lp;

    private float[] waitingTimes = { 1f, 0.7f, 0.5f, 0.3f, 0.2f };

    // anomalyCode: 0 if no anomaly, 1 if laptop anomaly, 2 if lava anomaly
    private int anomalyCode = 0;

    private bool power = false;
    
    public void Interact(GameObject obj) {
        
        switch (anomalyCode) {
            case 0:
                power = !power;
                screen.materials = power ? materialSet1 : materialSet2;
                break;
            case 1:
                power = !power;
                if (anomalyCode == 1 && power) StartCoroutine(ToggleLaptop1());
                break;
            case 2:
                StartCoroutine(ToggleLaptop2());
                break;
            case 3:
                //transform.SetParent();
                lp.Activate();
                break;
            default:
                break;
        }
    }

    private IEnumerator ToggleLaptop1() {
        foreach (float time in waitingTimes) {
            screen.materials = anomalyMaterialSet;
            yield return new WaitForSeconds(time);
            screen.materials = materialSet2;
            yield return new WaitForSeconds(0.2f);
        }
        power = false;
    }

    private IEnumerator ToggleLaptop2() {
        if (screenAni.GetBool("Opened")) {
            screenAni.SetBool("Opened", false);
            yield return new WaitForSeconds(1f);
            anomalyCode = 3;
        }
    }

    public void SetAnomalyCode(int code) => anomalyCode = code;

    public bool IsInteractable() {
        if (anomalyCode == 0 || anomalyCode == 3) return true;
        if (anomalyCode == 1) return !power;
        if (anomalyCode == 2) return screenAni.GetBool("Opened");
        return false;
    }
}
