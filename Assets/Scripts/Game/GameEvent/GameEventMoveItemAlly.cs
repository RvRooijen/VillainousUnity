public class GameEventMoveItemAlly : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        Villain.CurrentState = Villain.State.MoveItemAlly;
        return true;
    }
}