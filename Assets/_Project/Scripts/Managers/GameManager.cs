using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance = null;
    public static GameManager Instance {
        get
        {
            if (_instance == null)
            {
                //Find all singletons of this type in the scene
                GameManager[] allGameManagersInScene = FindObjectsOfType<GameManager>();

                if (allGameManagersInScene != null && allGameManagersInScene.Length > 0)
                {
                    //Destroy all but one singleton
                    if (allGameManagersInScene.Length > 1)
                    {
                        Debug.LogWarning($"You have more than one GameManager in the scene!");

                        for (int i = 1; i < allGameManagersInScene.Length; i++)
                        {
                            Destroy(allGameManagersInScene[i].gameObject);
                        }
                    }

                    //Now we should have just one singleton in the scene, so pick it
                    _instance = allGameManagersInScene[0];
                }
                //We have no singletons in the scene
                else
                {
                    Debug.LogError($"You need to add the script GameManager to gameobject in the scene!");
                }
            }

            return _instance;
        }
    }
    #endregion

    public PlayerCharacter playerCharacter;
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
