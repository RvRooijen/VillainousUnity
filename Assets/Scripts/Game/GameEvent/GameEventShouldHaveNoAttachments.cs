using System.Linq;

/// <summary>
/// Should contain cards of type to a minimal of amount
/// </summary>
public class GameEventShouldHaveNoAttachments : CardGameEvent
{
    public override bool Execute(Villain origin, params Card[] cards)
    {
        // Should contain cards of type to a minimal of amount
        return !Card.AttachedCards.Any();
    }
}