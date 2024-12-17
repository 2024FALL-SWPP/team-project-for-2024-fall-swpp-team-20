using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    private float speed = 5;
    public void ShowSlowly(float scale)
    {
        transform.localScale = new Vector3(1, 1, 0);
        StartCoroutine(ShowSlowlyCoroutine(scale));
    }

    private IEnumerator ShowSlowlyCoroutine(float scale)
    {
        float maxTime = scale / speed;
        float time = 0;
        while (time < maxTime)
        {
            time += Time.deltaTime;
            transform.localScale = new Vector3(1, 1, scale * time / maxTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
