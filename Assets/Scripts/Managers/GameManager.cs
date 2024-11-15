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

    public PlayManager pm;
    public UIManager um;
    public SoundManager sm;
    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }*/

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
            pm = GameObject.FindAnyObjectByType<PlayManager>().GetComponent<PlayManager>();
        }
        um = GameObject.FindAnyObjectByType<UIManager>().GetComponent<UIManager>();
        sm = GameObject.FindAnyObjectByType<SoundManager>().GetComponent<SoundManager>();
        pm.GameStart();
    }

    public void Clear() => state = GameState.GameClear;
    public void Pause() => state = GameState.Pause;
    public void Play() => state = GameState.Playing;

    public GameState GetState() => state;

}
