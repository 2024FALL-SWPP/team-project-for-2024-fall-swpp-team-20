using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class AnimationSceneController : MonoBehaviour
{
    public GameObject sleepCutObject;
    public GameObject wakeupCutObject;
    public Camera sleepCamera;
    public Camera wakeupCamera;

    private PlayableDirector sleepPlayable;
    private PlayableDirector wakeupPlayable;

    void Start()
    {
        if (sleepCutObject != null)
            sleepPlayable = sleepCutObject.GetComponent<PlayableDirector>();
        if (wakeupCutObject != null)
            wakeupPlayable = wakeupCutObject.GetComponent<PlayableDirector>();

        if (sleepCamera != null)
            sleepCamera.enabled = false;
        if (wakeupCamera != null)
            wakeupCamera.enabled = false;
    }

    void Update()
    {
        // F키를 누르면 SleepCut 실행
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            PlaySleepCut();
        }

        // G키를 누르면 WakeupCut 실행
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            PlayWakeupCut();
        }
    }

    void PlaySleepCut()
    {
        if (sleepPlayable != null)
        {
            sleepPlayable.Play();
        }
        if (sleepCamera != null)
        {
            sleepCamera.enabled = true;
        }
        if (wakeupCamera != null)
        {
            wakeupCamera.enabled = false;
        }
    }

    void PlayWakeupCut()
    {
        if (wakeupPlayable != null)
        {
            wakeupPlayable.Play();
        }
        if (wakeupCamera != null)
        {
            wakeupCamera.enabled = true;
        }
        if (sleepCamera != null)
        {
            sleepCamera.enabled = false;
        }
    }
}
