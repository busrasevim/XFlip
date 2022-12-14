
public class PlayOnFloor : PlayState
{
    public PlayOnFloor(PlayGameState.PlayStateType _playStateType, PlayState _toPlayState, PlayerCharacter _playerCharacter, PlayGameState _playGameState)
    {
        playState = _playStateType;
        toPlayState = _toPlayState;
        playerCharacter = _playerCharacter;

        playGameState = _playGameState;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!playerCharacter.IsTouchGround())
        {
            playGameState.NextState();
        }
    }
}
