using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance = null;
    public static GameManager Instance
    {
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

    public Character[] allAICharacters;
    public Character[] allCharacters;
    public List<Character> allOnGameCharacters = new List<Character>();

    public Transform finishObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private List<Character> characterList = new List<Character>();
    public void SetCharacterOrders()
    {
        foreach (var character in allOnGameCharacters)
        {
            character.ComputeDistance();
        }

        characterList.Clear();

        List<float> lengths = new List<float>();
        for (int i = 0; i < allOnGameCharacters.Count; i++)
        {
            lengths.Add(allOnGameCharacters[i].finishDistance);
            characterList.Add(allOnGameCharacters[i]);
        }

        lengths.Sort();

        for (int i = 0; i < lengths.Count; i++)
        {
            for (int j = 0; j < characterList.Count; j++)
            {
                if (lengths[i] == characterList[j].finishDistance)
                {
                    characterList[j].characterOrder = i + 1;

                    characterList.RemoveAt(j);
                    break;
                }
            }
        }
    }
}
