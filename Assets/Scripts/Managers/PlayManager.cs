using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private int stage;

    private bool haveAnomaly;

    private GameObject currentMap;
    private GameObject player;
    private PlayerController pc;

    private MapController mc;
    void Start()
    {
        stage = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        mc = FindObjectOfType<MapController>().GetComponent<MapController>();
        pc = player.GetComponent<PlayerController>();
        InitializeStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Call this when user interacts with bed with some interactino key
    public void OnBedInteraction(InputValue value) {
        if (pc.canSleep)
        {
            float input = value.Get<float>();
            StartCoroutine(BedInteraction(input > 0));
        }
    }

    public void TryBedInteraction(bool sleep) => StartCoroutine(BedInteraction(sleep));

    /// <summary>
    /// function called when player interacts with bed
    /// </summary>
    /// <param name="sleep">true when player decides sleep, false when player decides to wake up</param>
    public IEnumerator BedInteraction(bool sleep) {
        Debug.Log($"sleep: {sleep}");
        Debug.Log($"stage: {stage}");

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

    private void InitializeStage(int stage) {

        // TODO: Reset previous stage
        Destroy(currentMap);
        // TODO: Set Anomaly
        if (stage == 0 || Random.Range(0f, 1f) > 0.5) haveAnomaly = false;
        else haveAnomaly = true;
        // TODO: Reset Player position
        player.transform.position = new Vector3(0.41f, 0.5f, 0f);
        // Create new stage map
        currentMap = mc.GenerateMap(haveAnomaly);
        
        
    }


    private void Succeed(bool sleep)
    {
        // TODO: Animation
        Debug.Log("Hello from succeed");
        InitializeStage(++stage);
    }

    private void Fail(bool sleep) {
        // TODO: Animation
        Debug.Log("Hello from fial");
        if (stage > 1) stage--;
        InitializeStage(stage);
    }

    // Call When player restarts
    // Refactoring suggestion: implement without using scene reload
    public void Restart() => SceneManager.LoadScene("GameScene");
}
