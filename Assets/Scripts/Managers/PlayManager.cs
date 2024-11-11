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

    private GameObject landscapeObject;

    public Material[] landscapeMaterials;

    //only for anomaly testing
    private bool Test => mc.test;

    void Start()
    {
        stage = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        mc = FindObjectOfType<MapController>().GetComponent<MapController>();
        pc = player.GetComponent<PlayerController>();
        cc = FindObjectOfType<CameraController>().GetComponent<CameraController>();
        currentMap = GameObject.FindGameObjectWithTag("Map");
        landscapeObject = GameObject.Find("Landscape");
        InitializeStage(0);
    }

    // Update is called once per frame
    void Update()
    {

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

        // Reset previous stage
        Destroy(currentMap);
        // TODO: Set Anomaly
        if (!Test && (stage == 0 || Random.Range(0f, 1f) > 0.5)) haveAnomaly = false;
        else haveAnomaly = true;
        // Reset Player position
        player.transform.position = new Vector3(2.06f, 0.465f, -1.41f);
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

    private void ToggleInteraction(bool canInteract) { 
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
            Renderer renderer = landscapeObject.GetComponent<Renderer>();
            renderer.material = landscapeMaterials[stage];
        }
    }

    // Call When player restarts
    // Refactoring suggestion: implement without using scene reload
    public void Restart() => SceneManager.LoadScene("GameScene");
}
