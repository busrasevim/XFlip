using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager _instance = null;
    public static DataManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            GetDatas();
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    #endregion
    public int levelCount;

    private readonly string LevelData = "Level";
    private int _level;
    public int Level { get { return _level; } }


    private void GetDatas()
    {
        _level = PlayerPrefs.GetInt(LevelData);
    }

    public void SetLevel(int level)
    {
        _level = level;
        PlayerPrefs.SetInt(LevelData, _level);
    }
}
