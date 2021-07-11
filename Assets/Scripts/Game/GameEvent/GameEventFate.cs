public class GameEventFate : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        Villain.CurrentState = Villain.State.Fate;
        return true;
    }
}