
public class StartGameState : GameState
{
    public StartGameState(StateManager.GameStateType _gameStateType, GameState _toState)
    {
        gameState = _gameStateType;
        toGameState = _toState;
    }
}
