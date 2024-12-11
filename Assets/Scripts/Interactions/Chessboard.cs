using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessboard : InteractableObject
{
    private bool inAnomaly = false;
    private bool activated = false;

    protected override void Start()
    {

    }

    public override void Interact(GameObject obj)
    {
        activated = true;
        MoveToChessBoard(GameManager.GetInstance().player, obj);
    }

    private void MoveToChessBoard(GameObject player, GameObject chessboard)
    {
        GameObject black = chessboard.transform.Find("Chess_Board").Find("Black").gameObject;
        GameObject white = chessboard.transform.Find("Chess_Board").Find("White").gameObject;
        black.SetActive(false);
        PlayerController pc = player.GetComponent<PlayerController>();
        InteractionHandler handler = player.GetComponentInChildren<InteractionHandler>();
        pc.SetPlayerController(SpawnPosition.Chessboard);
        pc.SetAnomalyType(HardAnomalyCode.Chessboard);
        handler.SetMouseClickAction(1);
        GameManager.GetInstance().um.ShowHealthImage();
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.Chessboard);
        foreach (Pawn pawn in white.GetComponentsInChildren<Pawn>())
        {
            pawn.Activate();
        }
    }

    public override bool IsInteractable()
    {
        if (!inAnomaly) return false;
        else return !activated;
    }

    public void SetAnomaly()
    {
        inAnomaly = true;
        base.Start();
    }
}
