using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BedInteractionManager : MonoBehaviour
{

    private GameObject player;
    private PlayerController pc;
    private CameraController cc;
    private StageManager stageManager;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        cc = FindObjectOfType<CameraController>().GetComponent<CameraController>();
        stageManager = FindObjectOfType<StageManager>().GetComponent<StageManager>();
    }

    public void TryBedInteraction(bool sleep) => StartCoroutine(BedInteraction(sleep));

    public IEnumerator BedInteraction(bool sleep)
    {
        ToggleInteraction(false);

        if (sleep && stageManager.GetCurrentStage() == 0)
        {
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
            yield return null;
        }
        else
        {
            yield return HandleSleepWakeAnimation(sleep);
            stageManager.HandleSleepOutcome(sleep);
        }
    }

    private IEnumerator HandleSleepWakeAnimation(bool sleep)
    {
        if (sleep)
        {
            // Play sleep animation
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            // Play wake up animation
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ToggleInteraction(bool available)
    {
        pc.SetSleep(available);
        pc.SetMove(available);
        cc.SetInteract(available);
    }
}
