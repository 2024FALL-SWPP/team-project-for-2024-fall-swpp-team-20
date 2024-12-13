using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardChessAnomaly : HardAnomaly
{
    public override void Apply(GameObject map)
    {
        Chessboard board = storage.chessHitbox.GetComponent<Chessboard>();
        board.SetAnomaly();
        storage.chessWalls.SetActive(true);

    }

    public override HardAnomalyCode GetHardAnomalyCode()
    {
        return HardAnomalyCode.Chess;
    }
}
