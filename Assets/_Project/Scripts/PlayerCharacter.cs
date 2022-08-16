using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    // Start is called before the first frame update
    void Start()
    {

      //  fWheel.motorTorque = motorTorque * boostMultiple;
      //  bWheel.motorTorque = motorTorque * boostMultiple;
    }

    // Update is called once per frame
    void Update()
    {
        // MoveForward();
    }

    protected internal override void EndLevel(bool isWin)
    {
        base.EndLevel(isWin);

        Debug.Log("iswin: " + isWin);
        StateManager.Instance.NextState();  //temp line

        LevelManager.Instance.EndLevel(isWin);

    }
}
