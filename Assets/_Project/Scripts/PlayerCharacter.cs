using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    protected internal override void EndLevel(bool isWin)
    {
        if (isFinished) return;

        base.EndLevel(isWin);

        StateManager.Instance.NextState();  //temp line

        LevelManager.Instance.EndLevel(isWin);

    }
}
