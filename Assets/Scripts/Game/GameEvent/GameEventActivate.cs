public class GameEventActivate : GameEvent
{
    public override bool Execute(Villain origin, params Card[] cards)
    {
        return true;
    }
}