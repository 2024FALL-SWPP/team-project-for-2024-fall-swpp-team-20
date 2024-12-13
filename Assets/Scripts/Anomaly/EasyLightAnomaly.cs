using UnityEngine;

public class EasyLightAnomaly : Anomaly
{
    public override void Apply(GameObject map)
    {
        Light light = storage.anomalyLight.GetComponent<Light>();
        GameObject animationController = new GameObject("AnomalyAnimationController");
        animationController.transform.SetParent(map.transform);
        LightAnomalyAnimationController anomalyAnimationController = animationController.AddComponent<LightAnomalyAnimationController>();
        anomalyAnimationController.playAnimation(light);
    }
}