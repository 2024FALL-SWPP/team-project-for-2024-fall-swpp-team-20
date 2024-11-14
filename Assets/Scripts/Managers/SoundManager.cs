using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] pianoSounds = new AudioClip[8];

    public void PlayPianoSound(int keyIndex)
    {
        AudioSource.PlayClipAtPoint(pianoSounds[keyIndex], transform.position);
    }
}
