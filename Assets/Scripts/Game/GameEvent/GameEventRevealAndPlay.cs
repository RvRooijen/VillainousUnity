using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

public class GameEventRevealAndPlay : GameEvent
{
    [ShowInInspector]
    private CardManagement.DeckType _revealFromDeckType;

    [ShowInInspector]
    private Type _cardTypeToMatch;
    
    public override bool Execute(params Card[] cards)
    {
        switch (_revealFromDeckType)
        {
            case CardManagement.DeckType.Fate:
                List<Card> drawnCard = Villain.cardManagement.FateDeck.GetCardsFromDrawPile(1, true);
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
                        Villain.cardManagement.FateDeck.AddCardsToDrawPile(card);
                    }
                }
                break;
            case CardManagement.DeckType.Villain:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return true;
    }
}