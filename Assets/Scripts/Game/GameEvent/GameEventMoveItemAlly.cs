public class GameEventMoveItemAlly : GameEvent
{
    public override void Execute()
    {
        Villain.CurrentState = Villain.State.MoveItemAlly;
    }
}