using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateManager : MonoBehaviour
{
    #region Singleton
    private static StateManager _instance = null;
    public static StateManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }

        FillTheStates();

        LevelManager.Instance.StartAction += NextState;
    }
    #endregion

    public enum GameStateType
    {
        Start,
        Play,
        Finish
    }

    private GameState _currentGameState;
    private List<GameState> _gameStates = new List<GameState>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  //TEMP CODE
        {
            NextState(); 
        }

        OnUpdate();
    }
    public void OnUpdate()
    {
        _currentGameState?.OnUpdate();
    }

    public void NextState()
    {
        if (_currentGameState.toGameState != null)
        {
            SetState(_currentGameState.toGameState);
        }
        else
        {
            //oyun bitmiþ, finishten sonra çaðýrýlmýþ, sahne yeniden yüklenecek
        }
    }

    public void SetState(GameState state)
    {
        if (_currentGameState !=null && state == _currentGameState)
            return;

        _currentGameState?.OnExit();
        _currentGameState = state;

        _currentGameState?.OnEnter();
    }

    private void FillTheStates()
    {
        FinishGameState finishGameState = new FinishGameState(GameStateType.Finish, null);
        PlayGameState playGameState = new PlayGameState(GameStateType.Play, finishGameState);
        StartGameState startGameState = new StartGameState(GameStateType.Start, playGameState);

        _gameStates.Add(startGameState);
        _gameStates.Add(playGameState);
        _gameStates.Add(finishGameState);

        SetState(startGameState);
    }
}


