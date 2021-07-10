public class GameEventDiscard : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        if (Villain.CurrentState == Villain.State.SelectAction)
        {
            Villain.CurrentState = Villain.State.Discarding;
        }

        return true;
    }
}