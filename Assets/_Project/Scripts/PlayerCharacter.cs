using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // MoveForward();
    }

    protected internal override void EndLevel(bool isWin)
    {
        base.EndLevel(isWin);

        StateManager.Instance.NextState();  //temp line

        LevelManager.Instance.EndLevel(isWin);

    }
}
