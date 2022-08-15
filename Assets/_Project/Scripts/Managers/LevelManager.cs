using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    private void Start()
    {
        
    }

    public void StartLevel()
    {
        //level baþlar
        Debug.Log("startlevel");
        StartAction?.Invoke();
    }

    private void EndLevel(bool isWin)
    {
        EndAction?.Invoke(isWin);

        if (isWin)
        {
            NextLevel();
            //success ekraný çýkar
        }
        else
        {
            //fail ekraný çýkar
        }
    }

    private void NextLevel()
    {

    }
}
