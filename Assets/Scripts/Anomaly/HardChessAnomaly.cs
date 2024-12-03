using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardChessAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        ObjectStorage storage = map.GetComponent<ObjectStorage>();
        Chessboard board = storage.chessHitbox.GetComponent<Chessboard>();
        board.SetAnomaly();
        storage.chessWalls.SetActive(true);

    }

    public override void SetHardAnomalyCode()
    {
        GameManager.GetInstance().um.ShowCharacterScript(HardAnomalyCode.Chess);
    }

    public override HardAnomalyCode GetHardAnomalyCode()
    {
        return HardAnomalyCode.Chess;
    }
}
