public class GameEventPlayCard : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        Villain.CurrentState = Villain.State.PlayCard;
        return true;
    }
}