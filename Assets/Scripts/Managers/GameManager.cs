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
            DontDestroyOnLoad(this);
            instance.Initialize();
        }
        else Destroy(this);
    }

    private void Initialize(bool start = true)
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "GameScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            stageManager = GameObject.FindAnyObjectByType<StageManager>().GetComponent<StageManager>();
            bedInteractionManager = GameObject.FindAnyObjectByType<BedInteractionManager>().GetComponent<BedInteractionManager>();
            stageManager.InitializeVariables();
            bedInteractionManager.InitializeVariables();
        }
        um = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        sm = GameObject.FindAnyObjectByType<SoundManager>().GetComponent<SoundManager>();
        um.Initialize();

        stageManager.GameStart(start);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            Initialize(false);
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

}
