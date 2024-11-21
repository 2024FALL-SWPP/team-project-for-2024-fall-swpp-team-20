using System.Collections;
using UnityEditor;
using UnityEngine;

public class EasyBusHandleAnomaly : Anomaly
{
    GameObject[] busHandles;
    public override void Apply(GameObject map)
    {
        busHandles = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            busHandles[i] = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("bus_handle (" + i + ")").gameObject;
        }
        foreach (GameObject busHandle in busHandles)
        {
            GameObject animationController = new GameObject("AnomalyAnimationController");
            BusHandleAnomalyAnimationController anomalyAnimationController = animationController.AddComponent<BusHandleAnomalyAnimationController>();
            float duration = 300.0f;
            anomalyAnimationController.playAnimation(busHandle, duration);
            GameObject.Destroy(animationController, duration);
        }
    }
}