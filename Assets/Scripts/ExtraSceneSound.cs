using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSceneSound : MonoBehaviour
{
    private void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.volume = GameManager.GetInstance().GetBGMVolume();
    }
}
