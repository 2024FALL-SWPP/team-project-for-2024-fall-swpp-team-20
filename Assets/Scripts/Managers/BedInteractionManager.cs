using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private StageManager stageManager;


    public void InitializeVariables()
    {
        pc = player.GetComponent<PlayerController>();
        stageManager = FindObjectOfType<StageManager>().GetComponent<StageManager>();
    }

    public void TryBedInteraction(BedInteractionType type) => StartCoroutine(BedInteraction(type));

    public IEnumerator BedInteraction(BedInteractionType type)
    {
        if (stageManager.GetCurrentStage() == 0 && type == BedInteractionType.Wakeup)
        {
            yield break;
        }

        stageManager.ToggleActionAvailability(false);

        if (type == BedInteractionType.Sleep && stageManager.GetCurrentStage() == 0)
        {
            SceneManager.LoadScene("Stage0SleepScene");
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
            yield return null;
        }
        else
        {
            stageManager.HandleSleepOutcome(type);
        }
    }
}
