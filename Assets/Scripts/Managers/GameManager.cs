using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Pause,
    GameOver,
    GameClear
}

public class GameManager
{
    private volatile static GameManager instance;

    public GameState state;

    public StageManager stageManager;
    public BedInteractionManager bedInteractionManager;

    public UIManager um;
    public SoundManager sm;

    public static GameManager GetInstance() {
        if (instance == null) {
            instance = new GameManager();
            instance.Initialize();
        }
        return instance;
    }


    private void Initialize()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "GameScene")
        {
            stageManager = GameObject.FindAnyObjectByType<StageManager>().GetComponent<StageManager>();
            bedInteractionManager = GameObject.FindAnyObjectByType<BedInteractionManager>().GetComponent<BedInteractionManager>();
        }
        um = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        sm = GameObject.FindAnyObjectByType<SoundManager>().GetComponent<SoundManager>();
        um.Initialize();
        stageManager.GameStart();
    }

    public void Clear() => state = GameState.GameClear;
    public void Pause() => state = GameState.Pause;
    public void Play() => state = GameState.Playing;

    public void GameOver() => state = GameState.GameOver;

    public GameState GetState() => state;

}
