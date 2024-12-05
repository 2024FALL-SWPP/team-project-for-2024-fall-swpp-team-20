using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    protected MeshRenderer[] meshRenderers;

    // 각 렌더러별 원본 및 글로우 색상 배열
    private Material[] materials;
    private Color[] originalColors;
    private Color[] glowColors;

    private bool isGlowing = false;
    private Coroutine glowCoroutine;

    protected virtual void Start()
    {
        // 자식 오브젝트 포함 모든 MeshRenderer 가져오기
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>();
        List<MeshRenderer> filteredRenderers = new List<MeshRenderer>();

        foreach (Renderer renderer in allRenderers)
        {
            MeshRenderer meshRenderer = renderer as MeshRenderer;
            if (meshRenderer != null && meshRenderer.GetComponent<TMPro.TextMeshPro>() == null)
            {
                filteredRenderers.Add(meshRenderer);
            }
        }

        meshRenderers = filteredRenderers.ToArray();


        // 배열 초기화
        materials = new Material[meshRenderers.Length];
        originalColors = new Color[meshRenderers.Length];
        glowColors = new Color[meshRenderers.Length];

        // 각 렌더러의 재질, 원본 색상, 글로우 색상 저장
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            materials[i] = meshRenderers[i].material;
            originalColors[i] = materials[i].color;
            glowColors[i] = originalColors[i] + Color.white * 0.6f;
        }

        // var outline = gameObject.AddComponent<Outline>();
        // outline.OutlineMode = Outline.Mode.OutlineVisible;
        // outline.OutlineColor = Color.white;
        // outline.OutlineWidth = 3f;
    }

    public virtual void StartGlow()
    {
        // 이미 글로우 중이면 기존 코루틴 중지
        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
        }

        isGlowing = true;
        glowCoroutine = StartCoroutine(GlowEffect());
    }

    public virtual void EndGlow()
    {
        isGlowing = false;

        // 코루틴이 실행 중이었다면 중지
        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
            glowCoroutine = null;
        }

        ResetGlowEffect();
    }

    protected virtual IEnumerator GlowEffect()
    {
        while (isGlowing)
        {
            // 부드러운 반짝임 효과
            float t = (Mathf.Sin(Time.time * 4f) + 1f) * 0.5f;

            // 모든 렌더러의 색상 보간
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = Color.Lerp(originalColors[i], glowColors[i], t);
            }

            yield return null;
        }
    }

    protected virtual void ResetGlowEffect()
    {
        // 모든 렌더러의 원본 색상 복원
        if (materials != null)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = originalColors[i];
            }
        }
    }

    public abstract void Interact(GameObject obj);

    public abstract bool IsInteractable();
}
