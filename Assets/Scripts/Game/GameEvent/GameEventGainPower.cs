public class GameEventGainPower : GameEvent
{
    public int Value;

    public override bool Execute(params Card[] cards)
    {
        Villain.IncreasePower(Value);
        Villain.CurrentState = Villain.State.SelectAction;
        return true;
    }
}