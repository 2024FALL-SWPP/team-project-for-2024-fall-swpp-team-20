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

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    public PlayManager pm;
    public UIManager um;
    public SoundManager sm;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        initialize();
    }

    public void MoveToGameScene()
    {
        SceneManager.LoadScene("GameScene");
        StartCoroutine(InitializeAfterSceneLoad());
    }

    private IEnumerator InitializeAfterSceneLoad()
    {
        yield return new WaitForSeconds(0.1f);
        initialize();
    }

    private void initialize()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "GameScene")
        {
            pm = FindObjectOfType<PlayManager>().GetComponent<PlayManager>();
        }
        um = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        sm = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    public void Clear() => state = GameState.GameClear;
    public void Pause() => state = GameState.Pause;
    public void Play() => state = GameState.Playing;

    public GameState GetState() => state;

}
