using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : GameState
{
    public StartGameState(StateManager.GameStateType _gameStateType,GameState _toState)
    {
        gameState = _gameStateType;
        toGameState = _toState;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
