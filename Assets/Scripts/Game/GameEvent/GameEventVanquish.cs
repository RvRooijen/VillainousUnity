public class GameEventVanquish : GameEvent
{
    public override bool Execute(Villain origin, params Card[] cards)
    {
        return true;
    }
}