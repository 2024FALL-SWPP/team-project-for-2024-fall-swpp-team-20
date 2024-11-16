using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehaviour : MonoBehaviour
{
    private float initialPosX;
    private PlayerInformation pi;
    private void OnEnable()
    {
        initialPosX = transform.position.x;
        pi = FindAnyObjectByType<PlayerInformation>();
        pi.HurtPlayerByHeat();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
        if (transform.position.x > initialPosX + 50f) transform.Translate(Vector3.right * -50f);
    }


}
