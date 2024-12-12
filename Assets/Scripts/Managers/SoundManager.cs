using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] pianoSounds = new AudioClip[8];
    public AudioClip timeBombWarningSound;

    public AudioClip footstepSound;
    public AudioClip bombBeepSound;
    public AudioClip bombExplosionSound;
    public bool iswalking = false;
    public bool footstepSoundPlaying = false;
    public float footstepSoundVolume = 0.5f;

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

    public IEnumerator IPlayFootstepSound()
    {
        footstepSoundPlaying = true;
        GameObject player = GameManager.GetInstance().player;
        while (iswalking)
        {
            AudioSource.PlayClipAtPoint(footstepSound, player.transform.position, footstepSoundVolume);
            yield return new WaitForSeconds(0.5f);
        }
        footstepSoundPlaying = false;
    }

    public void PlayFootstepSound()
    {
        StartCoroutine(IPlayFootstepSound());
    }

    public void PlayBombBeepSound(GameObject bomb)
    {
        AudioSource.PlayClipAtPoint(bombBeepSound, bomb.transform.position);
    }

    public void PlayBombExplosionSound(GameObject bomb)
    {
        AudioSource.PlayClipAtPoint(bombExplosionSound, bomb.transform.position);
    }

}
