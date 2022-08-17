
public class PlayerCharacter : Character
{
    protected internal override void EndLevel(bool isWin,bool fromFail=false)
    {
        if (isFinished) return;

        base.EndLevel(isWin);

        StateManager.Instance.NextState();

        LevelManager.Instance.EndLevel(isWin);
    }
}
