public class CardGameEvent : GameEvent
{
    protected Card Card;

    public void Initialize(Villain villain, Card card)
    {
        Card = card;
        Initialize(villain);
    }
}