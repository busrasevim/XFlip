using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameState : GameState
{
    public PlayGameState(StateManager.GameStateType _gameStateType, GameState _toState)
    {
        gameState = _gameStateType;
        toGameState = _toState;

        FillTheStates();
    }

    public enum PlayStateType
    {
        PlayOnFloor,
        PlayOnSky
    }

    private PlayState _currentPlayState;
    private List<PlayState> playStates = new List<PlayState>();


    public override void OnUpdate()
    {
        base.OnUpdate();

        _currentPlayState?.OnUpdate();
    }

    public void NextState()
    {
        if (_currentPlayState.toPlayState != null)
        {
            SetState(_currentPlayState.toPlayState);
        }
        else
        {
            SetState(playStates[0]);
        }
    }

    private void SetState(PlayState state)
    {
        if (_currentPlayState != null && state == _currentPlayState)
            return;

        _currentPlayState?.OnExit();
        _currentPlayState = state;

        _currentPlayState?.OnEnter();
    }

    private void FillTheStates()
    {
        PlayOnSky playOnSky = new PlayOnSky(PlayStateType.PlayOnSky, null, GameManager.Instance.playerCharacter, this);
        PlayOnFloor playOnFloor = new PlayOnFloor(PlayStateType.PlayOnFloor, playOnSky, GameManager.Instance.playerCharacter, this);

        playStates.Add(playOnFloor);
        playStates.Add(playOnSky);

        SetState(playOnFloor);
    }
}
