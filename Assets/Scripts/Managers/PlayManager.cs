using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    private StageManager stageManager;
    private BedInteractionManager bedInteractionManager;
    void Start()
    {
        stageManager = FindObjectOfType<StageManager>().GetComponent<StageManager>();
        bedInteractionManager = FindObjectOfType<BedInteractionManager>().GetComponent<BedInteractionManager>();
    }

    public void GameStart()
    {
        GameManager.GetInstance().um.Initialize();
        GameManager.GetInstance().Play();
        stageManager.GameStart();
    }

    public void TryBedInteraction(bool sleep) => bedInteractionManager.TryBedInteraction(sleep);

    public void ToggleActionAvailability(bool canInteract)
    {
        stageManager.ToggleActionAvailability(canInteract);
    }
}
