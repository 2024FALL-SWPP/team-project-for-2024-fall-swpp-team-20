using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int currentStage;
    public int GetCurrentStage() => currentStage;
    private GameObject currentMap;
    private bool haveAnomaly;
    public bool GetHaveAnomaly() => haveAnomaly;
    private GameObject player;

    private MapManager mapManager;
    private PlayerController pc;
    private CameraController cc;

    private LandscapeManager landscapeManager;

    private bool Test => mapManager.test;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        cc = FindObjectOfType<CameraController>().GetComponent<CameraController>();
        mapManager = FindObjectOfType<MapManager>().GetComponent<MapManager>();
        currentMap = GameObject.FindGameObjectWithTag("Map");
        landscapeManager = FindObjectOfType<LandscapeManager>().GetComponent<LandscapeManager>();
    }

    public void GameStart()
    {
        currentStage = 0;
        pc.Initialize();
        cc.Initialize();
        mapManager.FillAnomaly();
        GameManager.GetInstance().Play();
        InitializeStage(currentStage);
    }

    public void InitializeStage(int stage)
    {
        if (stage == 7)
        {
            GameClear();
            return;
        }
        this.currentStage = stage;

        if (!Test && (stage == 0 || Random.Range(0f, 1f) > 0.5))
            haveAnomaly = false;
        else
            haveAnomaly = true;

        // Reset Player position
        player.transform.position = new Vector3(-19.5f, 0.2f, -7.31f);
        // Create new stage map
        mapManager.GenerateMap(haveAnomaly, stage);
        // Set time

        ToggleActionAvailability(true);
        landscapeManager.ChangeLandscape(stage);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleActionAvailability(bool canInteract)
    {
        pc.canSleep = canInteract;
        pc.canMove = canInteract;
        cc.canInteract = canInteract;
    }
    private void GameClear()
    {
        //DisableControllers();
        GameManager.GetInstance().Clear();
        GameManager.GetInstance().um.ShowStateUI(GameState.GameClear);
    }

    public void HandleSleepOutcome(bool sleep)
    {
        if (sleep ^ haveAnomaly)
            Succeed();
        else
            Fail();
    }

    private void Succeed()
    {
        // TODO: Animation
        InitializeStage(++currentStage);
    }

    private void Fail()
    {
        // TODO: Animation
        if (currentStage > 1) currentStage--;
        InitializeStage(currentStage);
    }
}
