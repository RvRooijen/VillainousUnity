public class GameEventVanquish : GameEvent
{
    public override bool Execute(Villain owner, params Card[] cards)
    {
        return true;
    }
}