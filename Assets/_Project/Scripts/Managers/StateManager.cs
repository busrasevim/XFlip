using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StateManager : MonoBehaviour
{
    #region Singleton
    private static StateManager _instance = null;
    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //Find all singletons of this type in the scene
                StateManager[] allStateManagersInScene = FindObjectsOfType<StateManager>();

                if (allStateManagersInScene != null && allStateManagersInScene.Length > 0)
                {
                    //Destroy all but one singleton
                    if (allStateManagersInScene.Length > 1)
                    {
                        Debug.LogWarning($"You have more than one StateManager in the scene!");

                        for (int i = 1; i < allStateManagersInScene.Length; i++)
                        {
                            Destroy(allStateManagersInScene[i].gameObject);
                        }
                    }

                    //Now we should have just one singleton in the scene, so pick it
                    _instance = allStateManagersInScene[0];
                }
                //We have no singletons in the scene
                else
                {
                    Debug.LogError($"You need to add the script StateManager to gameobject in the scene!");
                }
            }

            return _instance;
        }
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

    private void Awake()
    {
        FillTheStates();

        LevelManager.Instance.StartAction += NextState;
    }

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

    private void NextState()
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


