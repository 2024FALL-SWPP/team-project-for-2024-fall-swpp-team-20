
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayModeTestScript
{
    [UnityTest]
    public IEnumerator GameStartButtonLoadsGameScene()
    {
        yield return SceneManager.LoadSceneAsync("MainScene");

        var gameStartButton = GameObject.Find("StartButton");
        Assert.IsNotNull(gameStartButton, "'GameStart' 버튼이 존재하지 않습니다.");

        gameStartButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Stage0Animation");
        Assert.AreEqual("Stage0Animation", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator ManagersAreLoadedInGameScene()
    {
        yield return SceneManager.LoadSceneAsync("GameScene");

        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();

        Assert.IsNotNull(gameManager, "GameManager가 로드되지 않았습니다.");
        Assert.IsNotNull(gameManager.player, "player를 레퍼런스하고 있지 않습니다.");
        Assert.IsNotNull(gameManager.stageManager, "StageManager를 레퍼런스하고 있지 않습니다.");
        Assert.IsNotNull(gameManager.bedInteractionManager, "BedInteractionManager를 레퍼런스하고 있지 않습니다.");
        Assert.IsNotNull(gameManager.um, "UIManager를 레퍼런스하고 있지 않습니다.");
        Assert.IsNotNull(gameManager.sm, "SoundManager를 레퍼런스하고 있지 않습니다.");
    }

    [UnityTest]
    public IEnumerator ObjectStorageNotNullTest()
    {
        yield return SceneManager.LoadSceneAsync("GameScene");

        ObjectStorage objectStorage = GameObject.FindObjectOfType<ObjectStorage>();

        Assert.IsNotNull(objectStorage, "ObjectStorage가 로드되지 않았습니다.");
        Assert.IsNotNull(objectStorage.chessHitbox, "chessHitbox를 레퍼런스하고 있지 않습니다.");
        Assert.IsNotNull(objectStorage.chessWalls, "chessWalls를 레퍼런스하고 있지 않습니다.");
    }

}
