using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSceneSound : MonoBehaviour
{
    private void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.volume = GameManager.GetInstance().GetBGMVolume();
        }
    }
}
