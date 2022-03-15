using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

/// <summary>
/// Reveal a card for a deck type and if it matches the card type it will be played
/// </summary>
public class GameEventRevealAndPlay : CardGameEvent
{
    [ShowInInspector]
    private CardManagement.DeckType _revealFromDeckType;

    [ShowInInspector, OdinSerialize]
    private Type _cardTypeToMatch;
    
    public override bool Execute(Villain origin, params Card[] cards)
    {
        switch (_revealFromDeckType)
        {
            case CardManagement.DeckType.Fate:
                List<Card> drawnCard = Villain.cardManagement.FateDeck.RevealCardsFromDrawPile(1);
                if (drawnCard.Any())
                {
                    Card card = drawnCard.First();
                    if (card.GetType() == _cardTypeToMatch)
                    {
                        // Play card
                        origin.SetState(Villain.State.PlayFateCard, new PlayFateCardEventArgs{Card = card, Target = Villain});
                    }
                    else
                    {
                        return false;
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