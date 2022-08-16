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
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //Find all singletons of this type in the scene
                LevelManager[] allLevelManagersInScene = FindObjectsOfType<LevelManager>();

                if (allLevelManagersInScene != null && allLevelManagersInScene.Length > 0)
                {
                    //Destroy all but one singleton
                    if (allLevelManagersInScene.Length > 1)
                    {
                        Debug.LogWarning($"You have more than one LevelManager in the scene!");

                        for (int i = 1; i < allLevelManagersInScene.Length; i++)
                        {
                            Destroy(allLevelManagersInScene[i].gameObject);
                        }
                    }

                    //Now we should have just one singleton in the scene, so pick it
                    _instance = allLevelManagersInScene[0];
                }
                //We have no singletons in the scene
                else
                {
                    Debug.LogError($"You need to add the script LevelManager to gameobject in the scene!");
                }
            }

            return _instance;
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

        //bu level deðerini yükleyecek
        currentLevel = Instantiate(Resources.Load<GameObject>("Levels/Level_" + (level + 1))).GetComponent<Level>();
        currentLevel.ConstructLevel();
    }

    public void StartLevel()
    {
        //level baþlar
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
        //sahneyi yeniden yükleyecek yeni level deðeri ile
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
