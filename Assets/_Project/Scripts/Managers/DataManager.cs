using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton
    private static DataManager _instance = null;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //Find all singletons of this type in the scene
                DataManager[] allDataManagersInScene = FindObjectsOfType<DataManager>();

                if (allDataManagersInScene != null && allDataManagersInScene.Length > 0)
                {
                    //Destroy all but one singleton
                    if (allDataManagersInScene.Length > 1)
                    {
                        Debug.LogWarning($"You have more than one DataManager in the scene!");

                        for (int i = 1; i < allDataManagersInScene.Length; i++)
                        {
                            Destroy(allDataManagersInScene[i].gameObject);
                        }
                    }

                    //Now we should have just one singleton in the scene, so pick it
                    _instance = allDataManagersInScene[0];
                }
                //We have no singletons in the scene
                else
                {
                    Debug.LogError($"You need to add the script DataManager to gameobject in the scene!");
                }
            }

            return _instance;
        }
    }
    #endregion

    public int levelCount;

    private readonly string LevelData = "Level";
    private int _level;
    public int Level { get { return _level; } }

    private void Awake()
    {
        GetDatas();

        DontDestroyOnLoad(this);
    }

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
