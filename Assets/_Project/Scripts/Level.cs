using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int AICount;
    public Transform finishObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ConstructLevel()
    {
        GameManager.Instance.finishObject = finishObject;

        GameManager.Instance.playerCharacter.gameObject.SetActive(true);
        GameManager.Instance.allOnGameCharacters.Add(GameManager.Instance.playerCharacter);

        for (int i = 0; i < AICount; i++)
        {
            if (i < GameManager.Instance.allAICharacters.Length)
            {
                GameManager.Instance.allAICharacters[i].gameObject.SetActive(true);
                GameManager.Instance.allOnGameCharacters.Add(GameManager.Instance.allAICharacters[i]);
            }
        }

        for (int i = 0; i < GameManager.Instance.allOnGameCharacters.Count; i++)
        {
            GameManager.Instance.allOnGameCharacters[i].Construct();
        }
    }
}
