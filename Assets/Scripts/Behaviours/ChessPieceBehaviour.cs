using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPieceBehaviour : MonoBehaviour
{
    public static int DeadPawnCount = 0;
    public static int DeadPieceCount = 0;

    public int health;

    public abstract void Attack();
}
