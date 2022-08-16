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
            //bu s�re� boyunca d�necek, �ekilene kadar el
            playerCharacter.RotateMotorbike();
            //her at�lan takla say�lacak
            //ona g�re boost a��lacak
            //bir an bile el �ekilmi�se boost �ans� kaybolacak
        }
        else
        {
            //d�n���n pat diye durmas� �nlenecek
            playerCharacter.SetFlipCount(0,true);
        }

        if (playerCharacter.IsTouchGround())
        {
            playGameState.NextState();
        }
    }
}
