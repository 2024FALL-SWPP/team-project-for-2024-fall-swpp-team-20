using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour, IInteractable
{
    private bool inAnomaly = false;
    private bool activated = false;

    public void Interact(GameObject obj) {
        activated = true;
        MoveToChessBoard(GameManager.GetInstance().player, obj);
    }

    private void MoveToChessBoard(GameObject player, GameObject chessboard)
    {
        GameObject black = chessboard.transform.Find("Chess_Board").Find("Black").gameObject;
        GameObject white = chessboard.transform.Find("Chess_Board").Find("White").gameObject;
        black.SetActive(false);
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.SetTransform(SpawnPosition.Chessboard);
        pc.SetCameraClippingPlanes(0.02f);
        pc.SetPhysical(SpawnPosition.Chessboard);
        pc.SetAnomalyType(HardAnomalyCode.Chessboard);
        GameManager.GetInstance().um.ShowHealthImage();
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.Chessboard);
        foreach (Pawn pawn in white.GetComponentsInChildren<Pawn>()) {
            pawn.Activate();
        }
    }

    public bool IsInteractable() {
        if (!inAnomaly) return false;
        else return !activated;
    }

    public void SetAnomaly() => inAnomaly = true;
}
