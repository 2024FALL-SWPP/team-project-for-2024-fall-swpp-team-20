using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private int stage;

    private bool haveAnomaly;

    private GameObject currentMap;
    private GameObject player;
    private PlayerController pc;
    private MapController mc;
    private CameraController cc;

    private GameObject[] landscapeObjects;

    public Material[] landscapeMaterials;

    private GameState State => GameManager.instance.state;


    //only for anomaly testing
    private bool Test => mc.test;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mc = FindObjectOfType<MapController>().GetComponent<MapController>();
        pc = player.GetComponent<PlayerController>();
        cc = FindObjectOfType<CameraController>().GetComponent<CameraController>();
        currentMap = GameObject.FindGameObjectWithTag("Map");
        landscapeObjects = GameObject.FindGameObjectsWithTag("Landscape");

    }

    public void GameStart()
    {
        stage = 0;
        pc.Initialize();
        cc.Initialize();
        mc.FillAnomaly();
        GameManager.instance.um.Initialize();
        GameManager.instance.Play();
        InitializeStage(0);
    }

    public void TryBedInteraction(bool sleep) => StartCoroutine(BedInteraction(sleep));
    /// <summary>
    /// function called when player interacts with bed
    /// </summary>
    /// <param name="sleep">true when player decides sleep, false when player decides to wake up</param>
    public IEnumerator BedInteraction(bool sleep)
    {
        ToggleInteraction(false);

        if (sleep && stage == 0)
        {
            // Goes into inception
            InitializeStage(++stage);
            yield return null;
        }
        else
        {
            if (sleep)
            {
                // run sleeping animation
                yield return new WaitForSeconds(0.1f); // animation time is not defined, so should modify parameter
            }
            else
            {
                // run space crack animtion
                yield return new WaitForSeconds(0.1f); // animation time is not defined
            }
            if (sleep ^ haveAnomaly) Succeed(sleep);
            else Fail(sleep);
        }

    }

    private void InitializeStage(int stage)
    {
        if (stage == 7)
        {
            GameClear();
            return;
        }

        // Reset previous stage
        Destroy(currentMap);
        // TODO: Set Anomaly
        if (!Test && (stage == 0 || Random.Range(0f, 1f) > 0.5)) haveAnomaly = false;
        else haveAnomaly = true;
        // Reset Player position
        player.transform.position = new Vector3(-19.5f, 0.2f, -7.31f);
        // Create new stage map
        currentMap = mc.GenerateMap(haveAnomaly, stage);
        // Set time
        ToggleInteraction(true);
        ChangeLandscape(stage);

        // Fix Mouse cursor to center
        Cursor.lockState = CursorLockMode.Locked;
        /* TODO: If press ESC to pause, release mouse cursor
         * use this:
         * Cursor.lockState = CursorLockMode.None;
         */
    }

    private void ToggleInteraction(bool canInteract)
    {
        pc.canSleep = canInteract;
        pc.canMove = canInteract;
        cc.canInteract = canInteract;
    }
    private void Succeed(bool sleep)
    {
        // TODO: Animation
        InitializeStage(++stage);
    }

    private void Fail(bool sleep)
    {
        // TODO: Animation
        if (stage > 1) stage--;
        InitializeStage(stage);
    }

    private void ChangeLandscape(int stage)
    {
        if (stage >= 0 && stage < landscapeMaterials.Length)
        {
            foreach (GameObject landscapeObject in landscapeObjects)
            {
                Renderer renderer = landscapeObject.GetComponent<Renderer>();
                renderer.material = landscapeMaterials[stage];
            }
        }
    }

    private void GameClear()
    {
        //DisableControllers();
        GameManager.instance.Clear();
        GameManager.instance.um.ShowStateUI(GameState.GameClear);
    }

}
