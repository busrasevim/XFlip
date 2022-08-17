using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager _instance = null;
    public static LevelManager Instance { get { return _instance; } }

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
    }
    #endregion

    public event Action StartAction;
    public event Action<bool> EndAction;

    public Level currentLevel;

    private void Start() => ConstructLevel();

    private void ConstructLevel()
    {
        int level = DataManager.Instance.Level;
        if (level >= DataManager.Instance.levelCount)
        {
            level = Random.Range(0, DataManager.Instance.levelCount);
        }

        //bu level de�erini y�kleyecek
        currentLevel = Instantiate(Resources.Load<GameObject>("Levels/Level_" + (level + 1))).GetComponent<Level>();
        currentLevel.ConstructLevel();
    }

    public void StartLevel()
    {
        //level ba�lar
        StartAction?.Invoke();
    }

    public void EndLevel(bool isWin)
    {
        EndAction?.Invoke(isWin);

        if (isWin)
        {
            NextLevel();
        }
        else
        {

        }
    }

    private void NextLevel()
    {
        DataManager.Instance.SetLevel(DataManager.Instance.Level + 1);
    }

    public void LoadLevel()
    {
        //sahneyi yeniden y�kleyecek yeni level de�eri ile
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
