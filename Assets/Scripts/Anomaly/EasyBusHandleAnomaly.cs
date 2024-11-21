using System.Collections;
using UnityEditor;
using UnityEngine;

public class EasyBusHandleAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        GameObject busHandle = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Props").Find("bus_handle").gameObject;
        GameObject animationController = new GameObject("AnomalyAnimationController");
        BusHandleAnomalyAnimationController anomalyAnimationController = animationController.AddComponent<BusHandleAnomalyAnimationController>();
        float duration = 300.0f;
        anomalyAnimationController.playAnimation(busHandle, duration);
        GameObject.Destroy(animationController, duration);
    }
}