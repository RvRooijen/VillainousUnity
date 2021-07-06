public class GameEventDiscard : GameEvent
{
    public override void Execute()
    {
        if (Villain.CurrentState == Villain.State.SelectAction)
        {
            Villain.CurrentState = Villain.State.Discarding;
        }
    }
}