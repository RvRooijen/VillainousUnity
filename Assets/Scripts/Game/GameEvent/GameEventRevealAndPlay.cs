using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

public class GameEventRevealAndPlay : GameEvent
{
    [ShowInInspector]
    private Deck.DeckType _revealFromDeckType;

    [ShowInInspector]
    private Type _cardTypeToMatch;
    
    public override bool Execute(params Card[] cards)
    {
        switch (_revealFromDeckType)
        {
            case Deck.DeckType.Fate:
                List<Card> drawnCard = Villain.Deck.GetCards(Villain.Deck.FateDeck, 1);
                if (drawnCard.Any())
                {
                    Card card = drawnCard.First();
                    if (card.GetType() == _cardTypeToMatch)
                    {
                        // Play card
                    }
                    else
                    {
                        // Put card back in deck
                        Villain.Deck.FateDeck.Add(card);
                    }
                }
                break;
            case Deck.DeckType.Villain:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }
}