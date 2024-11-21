using UnityEngine;

public class EasyLightAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        Light light = map.transform.Find("Interior").Find("2nd Floor").Find("Apartment_01").Find("Lights").Find("Anomaly Light").GetComponent<Light>();
        GameObject animationController = new GameObject("AnomalyAnimationController");
        animationController.transform.SetParent(map.transform);
        LightAnomalyAnimationController anomalyAnimationController = animationController.AddComponent<LightAnomalyAnimationController>();
        anomalyAnimationController.playAnimation(light);
    }
}