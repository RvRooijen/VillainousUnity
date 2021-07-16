using System;

public class GameEventGainPower : GameEvent
{
    public int Value;

    public override bool Execute(Villain origin, params Card[] cards)
    {
        Villain.IncreasePower(Value);
        Villain.SetState(Villain.State.SelectAction, EventArgs.Empty);
        return true;
    }
}