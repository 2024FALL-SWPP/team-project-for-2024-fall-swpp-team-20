using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] pianoSounds = new AudioClip[8];
    public AudioClip timeBombWarningSound;

    public AudioClip footstepSound;
    public bool iswalking = false;
    public bool footstepSoundPlaying = false;

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

    public IEnumerator PlayFootstepSoundEnumerator()
    {
        footstepSoundPlaying = true;
        while (iswalking)
        {
            AudioSource.PlayClipAtPoint(footstepSound, transform.position);
            yield return new WaitForSeconds(0.5f);
        }
        footstepSoundPlaying = false;
    }

    public void PlayFootstepSound()
    {
        StartCoroutine(PlayFootstepSoundEnumerator());
    }

}
