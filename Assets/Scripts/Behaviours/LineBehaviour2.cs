using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour2 : MonoBehaviour
{
    private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoForward());
    }

    private IEnumerator GoForward()
    {
        float deltaTime;
        while (transform.localScale.x < 8 * 0.0609f)
        {
            deltaTime = Time.deltaTime;
            float moved = speed * 0.0609f * deltaTime;
            transform.Translate(transform.forward * moved, Space.World);
            transform.localScale += Vector3.right * 2 * moved;
            yield return null;
        }
        Destroy(gameObject);
    }

}
