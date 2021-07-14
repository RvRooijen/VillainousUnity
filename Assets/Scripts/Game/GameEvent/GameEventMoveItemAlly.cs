using System;

public class GameEventMoveItemAlly : GameEvent
{
    public override bool Execute(Villain owner, params Card[] cards)
    {
        Villain.SetState(Villain.State.PickMoveItemAlly, EventArgs.Empty);
        return true;
    }
}