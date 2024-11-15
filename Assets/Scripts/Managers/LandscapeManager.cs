using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeManager : MonoBehaviour
{
    private GameObject[] landscapeObjects;

    [SerializeField]
    private Material[] landscapeMaterials;

    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        landscapeObjects = GameObject.FindGameObjectsWithTag("Landscape");
    }

    public void ChangeLandscape(int stage)
    {
        if (stage >= 0 && stage < landscapeMaterials.Length)
        {
            foreach (GameObject landscapeObject in landscapeObjects)
            {
                Renderer renderer = landscapeObject.GetComponent<Renderer>();
                renderer.material = landscapeMaterials[stage];
            }
        }
    }
}
