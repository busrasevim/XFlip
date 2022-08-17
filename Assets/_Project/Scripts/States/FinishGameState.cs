using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameState : GameState
{
    public FinishGameState(StateManager.GameStateType _gameStateType, GameState _toState)
    {
        gameState = _gameStateType;
        toGameState = _toState;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

       // if (!GameManager.Instance.playerCharacter.isSucceed)
         //   GameManager.Instance.playerCharacter.SetWheelTorque(0f);
    }
}
