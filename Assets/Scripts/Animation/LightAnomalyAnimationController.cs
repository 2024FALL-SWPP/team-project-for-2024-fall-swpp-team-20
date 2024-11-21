using System.Collections;
using UnityEngine;

public class LightAnomalyAnimationController : MonoBehaviour
{
    public void playAnimation(Light light)
    {
        StartCoroutine(LightFlicker(light));
    }
    public IEnumerator LightFlicker(Light light)
    {
        float elapsedTime = 0;
        float flickerDuration;
        float flickerIntensity = 0.5f;
        float originalIntensity = light.GetComponent<Light>().intensity;
        while (light)
        {
            light.GetComponent<Light>().intensity = Random.Range(originalIntensity - flickerIntensity, originalIntensity + flickerIntensity);
            flickerDuration = Random.Range(0.3f, 0.7f);
            elapsedTime += flickerDuration;
            yield return new WaitForSeconds(flickerDuration);
        }
    }
}