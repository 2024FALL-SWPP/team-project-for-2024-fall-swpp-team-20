using System.Collections;
using UnityEditor;
using UnityEngine;

public class EasyBusHandleAnomaly : Anomaly
{
    GameObject[] busHandles;
    public override void Apply(GameObject map)
    {
        busHandles = storage.busHandles;
        foreach (GameObject busHandle in busHandles)
        {
            busHandle.SetActive(true);
            GameObject animationController = new GameObject("AnomalyAnimationController");
            animationController.transform.SetParent(map.transform);
            BusHandleAnomalyAnimationController anomalyAnimationController = animationController.AddComponent<BusHandleAnomalyAnimationController>();
            float duration = 300.0f;
            anomalyAnimationController.playAnimation(busHandle, duration);
            GameObject.Destroy(animationController, duration);
        }
    }
}