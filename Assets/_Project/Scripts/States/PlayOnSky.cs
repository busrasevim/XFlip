using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayOnSky : PlayState
{
    public PlayOnSky(PlayGameState.PlayStateType _playStateType, PlayState _toPlayState,PlayerCharacter _playerCharacter,PlayGameState _playGameState)
    {
        playState = _playStateType;
        toPlayState = _toPlayState;
        playerCharacter = _playerCharacter;

        playGameState = _playGameState;
    }


    public override void OnEnter()
    {
        base.OnEnter();

        playerCharacter.SetOnSkyAnimation(true);

       // playerCharacter.SetWheelTorque(0f);
    }

    public override void OnExit()
    {
        base.OnExit();

        playerCharacter.SetOnSkyAnimation(false);
        //   playerCharacter.SetWheelTorque();


        playerCharacter.motorBike.transform.DORotate(Vector3.zero, 1f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButton(0))
        {
            //bu süreç boyunca dönecek, çekilene kadar el
            playerCharacter.RotateMotorbike();
        }
        else
        {
            //dönüþün pat diye durmasý önlenecek
            playerCharacter.SetFlipCount(0,true);
        }

        if (playerCharacter.IsTouchGround())
        {
            playGameState.NextState();
        }

        playerCharacter.SetAngularVelocity();
    }
}
