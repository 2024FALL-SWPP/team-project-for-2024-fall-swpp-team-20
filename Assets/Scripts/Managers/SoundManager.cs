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

    private float bgmVolume;
    private float sfxVolume;
    private List<AudioSource> bgmList;
    private List<AudioSource> sfxList;

    public void Initialize()
    {
        bgmList = new List<AudioSource>();
        sfxList = new List<AudioSource>();
        bgmVolume = GameManager.GetInstance().GetBGMVolume();
        sfxVolume = GameManager.GetInstance().GetSFXVolume();
        AudioSource mainSource = GetComponent<AudioSource>();
        mainSource.volume = bgmVolume;
        bgmList.Add(mainSource);
    }
    public void PlayPianoSound(int keyIndex)
    {
        AudioSource.PlayClipAtPoint(pianoSounds[keyIndex], transform.position, sfxVolume);
    }

    public void PlayTimeBombWarningSound(GameObject timeBomb)
    {
        AudioSource audioSource = timeBomb.GetComponent<AudioSource>();
        sfxList.Add(audioSource);
        audioSource.clip = timeBombWarningSound;
        audioSource.loop = true;
        audioSource.spatialBlend = 1;
        audioSource.volume = sfxVolume;
        audioSource.Play();
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        foreach (AudioSource source in bgmList)
        {
            source.volume = bgmVolume;
        }
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        foreach (AudioSource source in sfxList)
        {
            source.volume = sfxVolume;
        }
    }
    public IEnumerator IPlayFootstepSound()
    {
        footstepSoundPlaying = true;
        GameObject player = GameManager.GetInstance().player;
        while (iswalking)
        {
            AudioSource.PlayClipAtPoint(footstepSound, player.transform.position, sfxVolume);
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
        AudioSource.PlayClipAtPoint(bombBeepSound, bomb.transform.position, sfxVolume);
    }

    public void PlayBombExplosionSound(GameObject bomb)
    {
        AudioSource.PlayClipAtPoint(bombExplosionSound, bomb.transform.position, sfxVolume);
    }

    public void PlayDrawerOpenSound(GameObject drawer)
    {
        AudioSource.PlayClipAtPoint(drawerOpenSound, drawer.transform.position, sfxVolume);
    }

    public void PlayDrawerCloseSound(GameObject drawer)
    {
        AudioSource.PlayClipAtPoint(drawerCloseSound, drawer.transform.position, sfxVolume);
    }

    public void PlayDoorOpenSound(GameObject door)
    {
        AudioSource.PlayClipAtPoint(doorOpenSound, door.transform.position, sfxVolume);
    }

    public void PlayDoorCloseSound(GameObject door)
    {
        AudioSource.PlayClipAtPoint(doorCloseSound, door.transform.position, sfxVolume);
    }

    public void PlayLavaSound(GameObject lava)
    {
        AudioSource lavaAudioSource = lava.AddComponent<AudioSource>();
        bgmList.Add(lavaAudioSource);
        lavaAudioSource.clip = lavaSound;
        lavaAudioSource.loop = true;
        lavaAudioSource.volume = bgmVolume;
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
