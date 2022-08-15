using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //Find all singletons of this type in the scene
                UIManager[] allUIManagersInScene = FindObjectsOfType<UIManager>();

                if (allUIManagersInScene != null && allUIManagersInScene.Length > 0)
                {
                    //Destroy all but one singleton
                    if (allUIManagersInScene.Length > 1)
                    {
                        Debug.LogWarning($"You have more than one UIManager in the scene!");

                        for (int i = 1; i < allUIManagersInScene.Length; i++)
                        {
                            Destroy(allUIManagersInScene[i].gameObject);
                        }
                    }

                    //Now we should have just one singleton in the scene, so pick it
                    _instance = allUIManagersInScene[0];
                }
                //We have no singletons in the scene
                else
                {
                    Debug.LogError($"You need to add the script UIManager to gameobject in the scene!");
                }
            }

            return _instance;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
