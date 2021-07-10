public class GameEventMoveHero : GameEvent
{
    public override bool Execute(params Card[] cards)
    {
        return true;
    }
}