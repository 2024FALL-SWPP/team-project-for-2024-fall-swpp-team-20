using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] pianoSounds = new AudioClip[8];
    public AudioClip timeBombWarningSound;

    public void PlayPianoSound(int keyIndex)
    {
        AudioSource.PlayClipAtPoint(pianoSounds[keyIndex], transform.position);
    }

    public void PlayTimeBombWarningSound(GameObject timeBomb)
    {
        AudioSource audioSource = timeBomb.GetComponent<AudioSource>();
        audioSource.clip = timeBombWarningSound;
        audioSource.loop = true;
        audioSource.spatialBlend = 1;
        audioSource.Play();
    }
}
