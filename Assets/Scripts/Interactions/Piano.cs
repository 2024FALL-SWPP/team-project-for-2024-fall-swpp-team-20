using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Piano : MonoBehaviour, IInteractable
{
    public GameObject[] pianoKeys = new GameObject[8];
    public bool inAnomaly = false;
    public Camera mainCamera;
    public Camera pianoCamera;
    private bool isInteracting = false;

    public PianoSystem pianoInput;




    private void Start()
    {

        mainCamera = Camera.main;
        pianoCamera = GameObject.Find("PianoCamera").GetComponent<Camera>();
        pianoCamera.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        pianoInput = new PianoSystem();
        pianoInput.Enable();
        pianoInput.PianoMap.PlayPiano.performed += OnPiano;
        pianoInput.PianoMap.Quit.performed += EndInteraction;
    }

    private void OnDisable()
    {
        pianoInput.PianoMap.PlayPiano.performed -= OnPiano;
        pianoInput.PianoMap.Quit.performed -= EndInteraction;
        pianoInput.Disable();
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
        GameManager.GetInstance().stageManager.ToggleActionAvailability(false);
        GameManager.GetInstance().um.ShowPianoInteractionInfo();
    }

    private void EndInteraction(InputAction.CallbackContext context)
    {
        if (GameManager.GetInstance().state != GameState.Playing) return;
        if (!isInteracting) return;
        isInteracting = false;
        pianoCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        GameManager.GetInstance().stageManager.ToggleActionAvailability(true);
        GameManager.GetInstance().um.HidePianoInteractionInfo();
    }

    public void Update()
    {
        /*if (isInteracting)
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
        }*/
    }

    private void OnPiano(InputAction.CallbackContext context)
    {
        if (!isInteracting) return;
        if (GameManager.GetInstance().state != GameState.Playing) return;
        int bindingIndex = context.action.GetBindingIndexForControl(context.control);
        StartCoroutine(PressKey(bindingIndex));
    }

    private IEnumerator PressKey(int keyIndex)
    {
        int realIndex = inAnomaly ? 7 - keyIndex : keyIndex;
        GameManager.GetInstance().sm.PlayPianoSound(realIndex);
        pianoKeys[realIndex].transform.position += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.1f);
        pianoKeys[realIndex].transform.position += new Vector3(0, 0.01f, 0);
    }

    public bool IsInteractable() => true;
}
