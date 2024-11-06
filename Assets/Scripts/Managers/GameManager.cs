using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayManager pm;
    public UIManager um;
    public SoundManager sm;
    public StateManager stm;
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
        if (activeScene == "GameScene" || activeScene == "DesignScene")
        {
            pm = FindObjectOfType<PlayManager>().GetComponent<PlayManager>();
            stm = FindObjectOfType<StateManager>().GetComponent<StateManager>();
        }
        um = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        sm = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }
}
