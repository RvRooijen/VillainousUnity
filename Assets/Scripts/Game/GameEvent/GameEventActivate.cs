public class GameEventActivate : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        return true;
    }
}