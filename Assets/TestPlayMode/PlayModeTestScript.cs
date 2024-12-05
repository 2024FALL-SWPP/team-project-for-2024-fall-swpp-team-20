
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

        Assert.IsTrue(objectStorage.bookCollection.Length == 2, "bookCollection");
        Assert.IsTrue(objectStorage.busHandles.Length == 4, "busHandles");

        Assert.IsNotNull(objectStorage.normalCanvas, "normalCanvas");
        Assert.IsNotNull(objectStorage.anomalCanvas, "anomalCanvas");

        Assert.IsNotNull(objectStorage.giraffeCanvas, "giraffeCanvas");

        Assert.IsNotNull(objectStorage.normalCanvasFlip, "normalCanvasFlip");

        Assert.IsNotNull(objectStorage.normalCube, "normalCube");
        Assert.IsNotNull(objectStorage.anomalyCube, "anomalyCube");

        Assert.IsNotNull(objectStorage.normalDice, "normalDice");
        Assert.IsNotNull(objectStorage.anomalyDice, "anomalyDice");

        Assert.IsNotNull(objectStorage.digitalClockText, "digitalClockText");

        Assert.IsNotNull(objectStorage.drawerMissingContent, "drawerMissingContent");

        Assert.IsNotNull(objectStorage.dresser, "dresser");
        Assert.IsNotNull(objectStorage.backOpenedDresser, "backOpenedDresser");

        Assert.IsTrue(objectStorage.hangers.Length == 4, "hangers");

        Assert.IsNotNull(objectStorage.laptopObject, "laptopObject");

        Assert.IsNotNull(objectStorage.anomalyLight, "anomalyLight");

        Assert.IsNotNull(objectStorage.pianoObject, "pianoObject");

        Assert.IsNotNull(objectStorage.playerAwake, "playerAwake");
        Assert.IsNotNull(objectStorage.playerSleeping, "playerSleeping");

        Assert.IsNotNull(objectStorage.sofa, "sofa");

        Assert.IsNotNull(objectStorage.spintopObject, "spintopObject");

        Assert.IsNotNull(objectStorage.normalTeddyBear, "normalTeddyBear");
        Assert.IsNotNull(objectStorage.anomalyTeddyBear, "anomalyTeddyBear");

        Assert.IsNotNull(objectStorage.chessHitbox, "chessHitbox");
        Assert.IsNotNull(objectStorage.chessWalls, "chessWalls");

        Assert.IsNotNull(objectStorage.bed, "bed");

        Assert.IsNotNull(objectStorage.digitalClock, "digitalClock");
        Assert.IsNotNull(objectStorage.timeBomb, "timeBomb");
    }

}
