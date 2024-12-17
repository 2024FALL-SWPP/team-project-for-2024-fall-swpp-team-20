using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class Laptop : InteractableObject
{
    public Material materialOn;       // 전원이 켜졌을 때의 Material
    public Material materialOff;      // 전원이 꺼졌을 때의 Material
    public Material anomalyMaterial;  // 이상 상태일 때의 Material
    public Renderer screen;           // Material을 적용할 Renderer
    private bool inAnomaly = false;
    private bool power = false;
    private int screenMaterialIndex = -1; // "screen" Material의 인덱스 저장


    protected override void Start()
    {
        base.Start();
        FindScreenMaterialIndex(); // Start에서 인덱스를 한 번 찾음
    }

    public override void Interact(GameObject obj)
    {
        if (inAnomaly)
        {
            power = !power;
            if (power) StartCoroutine(ToggleLaptop1());
        }
        else
        {
            if (power) TurnOff();
            else TurnOn();
        }

    }

    private void TurnOn()
    {
        power = true;
        ReplaceScreenMaterial(materialOn); // 전원이 켜졌을 때의 Material 적용
    }

    private void TurnOff()
    {
        power = false;
        ReplaceScreenMaterial(materialOff); // 전원이 꺼졌을 때의 Material 적용
    }

    private IEnumerator ToggleLaptop1()
    {
        ReplaceScreenMaterial(anomalyMaterial);
        yield return new WaitForSeconds(0.1f);
        ReplaceScreenMaterial(materialOff);
        yield return new WaitForSeconds(0.2f);
        power = false;
    }

    private void FindScreenMaterialIndex()
    {
        Material[] materials = screen.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].name == "screen (Instance)" || materials[i].name == "screen")
            {
                screenMaterialIndex = i; // "screen" Material의 인덱스를 저장
                // Debug.Log("Screen material found at index: " + screenMaterialIndex);
                return; // 첫 번째로 찾은 "screen" Material만 사용
            }
        }

        Debug.LogWarning("No matching 'screen' material found.");
    }

    private void ReplaceScreenMaterial(Material newMaterial)
    {
        if (screenMaterialIndex == -1)
        {
            Debug.LogWarning("Screen material index is not set. Did you forget to call FindScreenMaterialIndex?");
            return;
        }

        Material[] materials = screen.materials;

        // 저장된 인덱스를 기반으로 Material 교체
        if (screenMaterialIndex >= 0 && screenMaterialIndex < materials.Length)
        {
            materials[screenMaterialIndex] = newMaterial;
            screen.materials = materials; // 변경 내용을 적용
            // Debug.Log("Material replaced at index " + screenMaterialIndex + " with " + newMaterial.name);
        }
    }

    public void SetAnomaly() => inAnomaly = true;

    public override bool IsInteractable()
    {
        if (inVisibilityAnomaly) return false;
        if (inAnomaly) return !power;
        else return true;
    }


}
