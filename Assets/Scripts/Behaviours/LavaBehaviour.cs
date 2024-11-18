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


}
