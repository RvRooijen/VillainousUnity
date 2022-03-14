using System;

public class GameEventVanquish : GameEvent
{
    public override bool Execute(Villain origin, params Card[] cards)
    {
        if (Villain.CurrentState == Villain.State.SelectAction)
        {
            Villain.SetState(Villain.State.ChooseVanquishTarget, EventArgs.Empty);
        }

        return true;
    }
}