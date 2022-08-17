using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayState : State
{
    public PlayGameState.PlayStateType playState;
    public PlayState toPlayState;

    public PlayerCharacter playerCharacter;

    public PlayGameState playGameState;

    public override void OnUpdate()
    {
        base.OnUpdate();


        playerCharacter.SetCharacterOrderText();
        playerCharacter.SetWheelAnimationsSpeed();
        playerCharacter.ClampVelocity();
    }
}
