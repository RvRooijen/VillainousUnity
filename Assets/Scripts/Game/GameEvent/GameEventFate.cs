using System;

public class GameEventFate : GameEvent
{
    public override bool Execute(Villain owner, params Card[] cards)
    {
        Villain.SetState(Villain.State.ChooseFateTarget, EventArgs.Empty);
        return true;
    }
}