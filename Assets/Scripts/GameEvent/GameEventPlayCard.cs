public class GameEventPlayCard : GameEvent
{
    public override void Execute()
    {
        Villain.CurrentState = Villain.State.PlayCard;
    }
}