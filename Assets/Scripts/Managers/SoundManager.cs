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
    public AudioClip drawerOpenSound;
    public AudioClip drawerCloseSound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip lavaSound;
    public AudioClip easyStageSound;
    public AudioClip hardStageSound;
    public bool iswalking = false;
    public bool footstepSoundPlaying = false;
    public float footstepSoundVolume = 0.5f;

    public float generalVolume;
    private List<AudioSource> sourceList;

    public void Initialize() { 
        sourceList = new List<AudioSource>();
        generalVolume = GameManager.GetInstance().volume;
        AudioSource mainSource = GetComponent<AudioSource>();
        sourceList.Add(mainSource);
    }
    public void PlayPianoSound(int keyIndex)
    {
        AudioSource.PlayClipAtPoint(pianoSounds[keyIndex], transform.position);
    }

    public void PlayTimeBombWarningSound(GameObject timeBomb)
    {
        AudioSource audioSource = timeBomb.GetComponent<AudioSource>();
        sourceList.Add(audioSource);
        audioSource.clip = timeBombWarningSound;
        audioSource.loop = true;
        audioSource.spatialBlend = 1;
        audioSource.volume = generalVolume;
        audioSource.Play();
    }

    public void SetVolume(float value)
    {
        Debug.Log("HELlo WORLd");
        generalVolume = value;
        foreach (AudioSource source in sourceList) {
            source.volume = generalVolume;
        }
    }

    public IEnumerator IPlayFootstepSound()
    {
        footstepSoundPlaying = true;
        GameObject player = GameManager.GetInstance().player;
        while (iswalking)
        {
            AudioSource.PlayClipAtPoint(footstepSound, player.transform.position, generalVolume);
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
        AudioSource.PlayClipAtPoint(bombBeepSound, bomb.transform.position, generalVolume);
    }

    public void PlayBombExplosionSound(GameObject bomb)
    {
        AudioSource.PlayClipAtPoint(bombExplosionSound, bomb.transform.position, generalVolume);
    }

    public void PlayDrawerOpenSound(GameObject drawer)
    {
        AudioSource.PlayClipAtPoint(drawerOpenSound, drawer.transform.position, generalVolume);
    }

    public void PlayDrawerCloseSound(GameObject drawer)
    {
        AudioSource.PlayClipAtPoint(drawerCloseSound, drawer.transform.position, generalVolume);
    }

    public void PlayDoorOpenSound(GameObject door)
    {
        AudioSource.PlayClipAtPoint(doorOpenSound, door.transform.position, generalVolume);
    }

    public void PlayDoorCloseSound(GameObject door)
    {
        AudioSource.PlayClipAtPoint(doorCloseSound, door.transform.position, generalVolume);
    }

    public void PlayLavaSound(GameObject lava)
    {
        AudioSource lavaAudioSource = lava.AddComponent<AudioSource>();
        sourceList.Add(lavaAudioSource);
        lavaAudioSource.clip = lavaSound;
        lavaAudioSource.loop = true;
        lavaAudioSource.volume = generalVolume;
        lavaAudioSource.Play();
    }

    public void PlayEasyStageSound()
    {
        AudioSource.PlayClipAtPoint(easyStageSound, transform.position);
    }

    public void PlayHardStageSound()
    {
        AudioSource.PlayClipAtPoint(hardStageSound, transform.position);
    }
}
