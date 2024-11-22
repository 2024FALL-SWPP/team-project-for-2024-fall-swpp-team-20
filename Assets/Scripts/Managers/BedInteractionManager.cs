using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum BedInteractionType
{
    Sleep,
    Wakeup,
    ClearHard
}

public class BedInteractionManager : MonoBehaviour
{

    private GameObject player => GameManager.GetInstance().player;
    private PlayerController pc;
    private InteractionHandler interactionHandler;
    private StageManager stageManager;


    public void InitializeVariables()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        interactionHandler = FindObjectOfType<InteractionHandler>().GetComponent<InteractionHandler>();
        stageManager = FindObjectOfType<StageManager>().GetComponent<StageManager>();
    }

    public void TryBedInteraction(BedInteractionType type) => StartCoroutine(BedInteraction(type));

    public IEnumerator BedInteraction(BedInteractionType type)
    {
        ToggleInteraction(false);

        if (type == BedInteractionType.Sleep && stageManager.GetCurrentStage() == 0)
        {
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
            yield return null;
        }
        else if (type == BedInteractionType.ClearHard)
        {
            // Do Something
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
        }
        else
        {
            yield return HandleSleepWakeAnimation(type);
            stageManager.HandleSleepOutcome(type);
        }
    }

    private IEnumerator HandleSleepWakeAnimation(BedInteractionType type)
    {
        if (type == BedInteractionType.Sleep)
        {
            // Play sleep animation
            yield return new WaitForSeconds(0.1f);
        }
        else if (type == BedInteractionType.Wakeup)
        {
            // Play wake up animation
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ToggleInteraction(bool available)
    {
        pc.SetSleep(available);
        pc.SetMove(available);
        interactionHandler.SetInteract(available);
    }
}
