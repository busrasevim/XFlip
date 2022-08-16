using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public override void OnExit()
    {
        base.OnExit();

        playerCharacter.SetOnSkyAnimation(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButton(0))
        {
            //bu süreç boyunca dönecek, çekilene kadar el
            playerCharacter.RotateMotorbike();
            //her atýlan takla sayýlacak
            //ona göre boost açýlacak
            //bir an bile el çekilmiþse boost þansý kaybolacak
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
    }
}
