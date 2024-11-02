using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    GameOver,
    GameClear
}

public class StateManager : MonoBehaviour
{
    public GameState state;
}
