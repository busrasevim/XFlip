using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : State
{
    public StateManager.GameStateType gameState;
    public GameState toGameState;
}
