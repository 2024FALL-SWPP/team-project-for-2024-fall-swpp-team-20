using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BedInteractionType
{
    Sleep,
    Wakeup,
    ClearHard,
    FailHard,
}

public class BedInteractionManager : MonoBehaviour
{
    private GameObject player => GameManager.GetInstance().player;
    private PlayerController pc;
    private StageManager stageManager;

    public void InitializeVariables()
    {
        pc = player.GetComponent<PlayerController>();
        stageManager = FindObjectOfType<StageManager>().GetComponent<StageManager>();
    }

    public void TryBedInteraction(BedInteractionType type) => StartCoroutine(BedInteraction(type));

    public IEnumerator BedInteraction(BedInteractionType type)
    {
        if (stageManager.GetCurrentStage() == 0 && type == BedInteractionType.Wakeup)
        {
            yield break;
        }

        stageManager.ToggleActionAvailability(false);

        if (type == BedInteractionType.Sleep && stageManager.GetCurrentStage() == 0)
        {
            // 씬 로드 후 Stage 업데이트
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Stage0SleepScene");
            yield return null; // Scene 전환 중에는 코루틴 일시 정지
        }
        else
        {
            stageManager.HandleSleepOutcome(type);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Stage0SleepScene")
        {
            // Stage를 +1로 업데이트
            stageManager.InitializeStage(stageManager.GetCurrentStage() + 1);
            // 이벤트 해제
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
