using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeManager : MonoBehaviour
{
    public Material landscapeMaterial;

    void Start()
    {
        InitializeSkybox();
    }

    private void InitializeSkybox()
    {
        RenderSettings.skybox = landscapeMaterial;
        SetSkyboxExposure(0);
    }

    public void ChangeLandscape(int stage)
    {
        SetSkyboxExposure(stage);
    }

    private void SetSkyboxExposure(int stage)
    {
        if (RenderSettings.skybox != null)
        {
            float exposureValue = stage * 0.1f;
            RenderSettings.skybox.SetFloat("_Exposure", exposureValue);
        }
    }
}
