using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour, IInteractable
{
    public GameObject[] pianoKeys = new GameObject[8];
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
            for (int i = 0; i < 8; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    GameManager.instance.sm.PlayPianoSound(i);
                    StartCoroutine(PressKey(i));
                }
            }
        }
    }

    private IEnumerator PressKey(int keyIndex)
    {
        pianoKeys[keyIndex].transform.position += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.1f);
        pianoKeys[keyIndex].transform.position += new Vector3(0, 0.01f, 0);
    }
}
