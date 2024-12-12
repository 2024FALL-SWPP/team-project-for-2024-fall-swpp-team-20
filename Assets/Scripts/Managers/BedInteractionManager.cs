using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public enum BedInteractionType
{
    Sleep,
    Wakeup,
    ClearHard,
    FailHard,
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
        if (stageManager.GetCurrentStage() == 0 && type == BedInteractionType.Wakeup)
        {
            yield break;
        }

        ToggleInteraction(false);

        if (type == BedInteractionType.Sleep && stageManager.GetCurrentStage() == 0)
        {
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
            yield return null;
        }
        else
        {
            stageManager.HandleSleepOutcome(type);
        }
    }

    public void ToggleInteraction(bool available)
    {
        pc.SetSleep(available);
        pc.SetMove(available);
        interactionHandler.SetInteract(available);
    }
}
