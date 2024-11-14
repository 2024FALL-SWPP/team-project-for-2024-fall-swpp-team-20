using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Piano : MonoBehaviour, IInteractable
{
    public GameObject[] pianoKeys = new GameObject[8];
    public AudioClip[] pianoSounds = new AudioClip[8];
    public bool inAnomaly = false;
    public Camera mainCamera;
    public Camera pianoCamera;
    private bool isInteracting = false;

    private void Start()
    {
        mainCamera = Camera.main;
        pianoCamera = GameObject.Find("PianoCamera").GetComponent<Camera>();
        pianoCamera.gameObject.SetActive(false);
    }

    public void Interact(GameObject obj)
    {
        StartInteraction();
    }

    private void StartInteraction()
    {
        isInteracting = true;
        mainCamera.gameObject.SetActive(false);
        pianoCamera.gameObject.SetActive(true);
        GameManager.instance.pm.ToggleInteraction(false);
        GameManager.instance.um.ShowPianoInteractionInfo();
    }

    private void EndInteraction()
    {
        isInteracting = false;
        pianoCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        GameManager.instance.pm.ToggleInteraction(true);
        GameManager.instance.um.HidePianoInteractionInfo();
    }

    public void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                EndInteraction();
            }
        }
    }

    public void OnPlayPiano(InputValue value)
    {
        if (isInteracting)
        {
            int keyIndex = value.Get<int>();
            StartCoroutine(PressKey(keyIndex));
            if (inAnomaly)
            {
                PlayKeySound(7 - keyIndex);
            }
            else
            {
                PlayKeySound(keyIndex);
            }
        }
    }

    private void PlayKeySound(int keyIndex)
    {
        AudioSource.PlayClipAtPoint(pianoSounds[keyIndex], transform.position);
    }

    private IEnumerator PressKey(int keyIndex)
    {
        pianoKeys[keyIndex].transform.position += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.1f);
        pianoKeys[keyIndex].transform.position += new Vector3(0, 0.01f, 0);
    }
}
