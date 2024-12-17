using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Pause,
    ReadingScript,
    GameOver,
    GameClear
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameState state;

    public StageManager stageManager;
    public BedInteractionManager bedInteractionManager;

    public UIManager um;
    public SoundManager sm;

    public GameObject player;

    [Header("Settings")]
    private float sensitivity;
    private float volume;

    private bool started;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("There should be an GameObject object");
            return null;
        }
        else return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.InitializeSetting();
            DontDestroyOnLoad(this);
            instance.Initialize();
        }
        else Destroy(this);
    }

    private void InitializeSetting()
    {
        sensitivity = 50f;
        volume = 1f;
        started = false;
    }

    private void Initialize(bool start = true)
    {
        string activeScene = SceneManager.GetActiveScene().name;
        um = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        sm = GameObject.FindAnyObjectByType<SoundManager>().GetComponent<SoundManager>();
        um.Initialize();
        sm.Initialize();
        if (activeScene == "GameScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            stageManager = GameObject.FindAnyObjectByType<StageManager>().GetComponent<StageManager>();
            bedInteractionManager = GameObject.FindAnyObjectByType<BedInteractionManager>().GetComponent<BedInteractionManager>();
            stageManager.InitializeVariables();
            bedInteractionManager.InitializeVariables();
            stageManager.GameStart(start);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            if (started) Initialize(false);
            else
            {
                started = true;
                Initialize(true);
            }
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

    public void Clear() => state = GameState.GameClear;
    public void Pause() => state = GameState.Pause;
    public void Play() => state = GameState.Playing;

    public void GameOver() => state = GameState.GameOver;

    public void ReadScript() => state = GameState.ReadingScript;

    public GameState GetState() => state;

    public void SetVolume(float value)
    {
        volume = value / 100f;
        Debug.Assert(sm != null, "NULL?!");
        sm.SetVolume(volume);
    }

    public float GetVolume() => volume;
    public void SetSensitivity(float value)
    {
        sensitivity = value;
    }

    public float GetSensitivity() => sensitivity;

    public void ResetGame()
    {
        started = false;
        //TODO: save achievement state
        SceneManager.LoadScene("MainScene");
    }
}
