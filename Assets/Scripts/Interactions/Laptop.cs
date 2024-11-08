using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour, IInteractable
{
    public Material[] materialSet1;
    public Material[] materialSet2;
    public Material[] anomalyMaterialSet;
    public Renderer screen;

    private float[] waitingTimes = { 1f, 0.7f, 0.5f, 0.3f, 0.2f };

    public bool inAnomaly = false;

    private bool power = false;
    
    public void Interact(GameObject obj) {
        power = !power;
        if (inAnomaly && power)  StartCoroutine(ToggleLaptop());
        else screen.materials = power ? materialSet1 : materialSet2;
    }

    public IEnumerator ToggleLaptop() {
        foreach (float time in waitingTimes) {
            screen.materials = anomalyMaterialSet;
            yield return new WaitForSeconds(time);
            screen.materials = materialSet2;
            yield return new WaitForSeconds(0.2f);
        }
        power = false;
    }

}
