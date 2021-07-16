using System;

public class GameEventDiscard : GameEvent
{
    public override bool Execute(Villain origin, params Card[] cards)
    {
        if (Villain.CurrentState == Villain.State.SelectAction)
        {
            Villain.SetState(Villain.State.Discarding, EventArgs.Empty);
        }

        return true;
    }
}