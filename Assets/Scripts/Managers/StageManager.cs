using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int currentStage;
    public int GetCurrentStage() => currentStage;
    private GameObject currentMap;
    private bool haveAnomaly;
    public bool GetHaveAnomaly() => haveAnomaly;
    private GameObject player => GameManager.GetInstance().player;
    private MapController mc;
    private PlayerController pc;
    private CameraController cc;
    private PlayerInformation pi;
    private LandscapeManager landscapeManager;
    private bool Test => mc.test;

    public void InitializeVariables()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        cc = FindObjectOfType<CameraController>();
        mc = FindObjectOfType<MapController>();
        pi = player.GetComponent<PlayerInformation>();
        currentMap = GameObject.FindGameObjectWithTag("Map");
        landscapeManager = FindObjectOfType<LandscapeManager>();
    }

    public void GameStart()
    {
        currentStage = 0;
        pc.Initialize();
        cc.Initialize();
        mc.FillAnomaly();
        GameManager.GetInstance().Play();
        InitializeStage(currentStage);
    }

    public void InitializeStage(int stage)
    {
        
        currentStage = stage;

        if (!Test && (stage == 0 || stage == 7 || Random.Range(0f, 1f) > 0.5))
            haveAnomaly = false;
        else
            haveAnomaly = true;

        // Reset UI
        GameManager.GetInstance().um.HideEverything();
        // Reset Player position, scale and Information
        player.transform.position = new Vector3(-19.25f, 0.2f, -7.4f);
        // player.transform.localScale = 0.13f * Vector3.one;
        pi.Initialize();

        // Create new stage map and inform player about it is hard anomaly or not
        bool hard = mc.GenerateMap(haveAnomaly, stage);

        if (stage == 7)
        {
            GameClear();
            return;
        }

        pc.SetAnomalyType(hard);
        // Set time
        ToggleActionAvailability(true);
        landscapeManager.ChangeLandscape(stage);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToggleActionAvailability(bool available)
    {
        pc.SetSleep(available);
        pc.SetMove(available);
        cc.SetInteract(available);
    }
    private void GameClear()
    {
        //DisableControllers();
        GameManager.GetInstance().Clear();
        GameManager.GetInstance().um.ShowStateUI(GameState.GameClear);
    }

    public void GameOver() {
        GameManager.GetInstance().GameOver();
        GameManager.GetInstance().um.ShowStateUI(GameState.GameOver);
    }
    public void HandleSleepOutcome(BedInteractionType type)
    {
        bool sleep = type == BedInteractionType.Sleep;
        if (sleep && haveAnomaly)
        {
            Fail();
            InitializeStage(currentStage); //TODO
        }
        else if (!sleep && !haveAnomaly)
        {
            Fail();
            SceneManager.LoadScene("AnomalyFalseWakeupScene");
        }
        else if (sleep && !haveAnomaly)
        {
            Succeed();
            InitializeStage(currentStage); //TODO
        }
        else if (!sleep && haveAnomaly)
        {
            Succeed();
            SceneManager.LoadScene("AnomalyTrueWakeupScene");
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            GameManager instance = GameManager.GetInstance();
            instance.um.Initialize();
            InitializeStage(currentStage);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Succeed()
    {
        ++currentStage;
    }

    private void Fail()
    {
        // TODO: Animation
        if (currentStage > 1) currentStage--;
    }
}
