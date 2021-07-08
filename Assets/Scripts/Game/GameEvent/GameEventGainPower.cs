public class GameEventGainPower : GameEvent
{
    public int Value;

    public override void Execute()
    {
        Villain.IncreasePower(Value);
        Villain.CurrentState = Villain.State.SelectAction;
    }
}