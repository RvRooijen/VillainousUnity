using System;

public class GameEventPlayCard : GameEvent
{
    public override bool Execute(Villain owner, params Card[] cards)
    {
        Villain.SetState(Villain.State.PlayCard, EventArgs.Empty);
        return true;
    }
}