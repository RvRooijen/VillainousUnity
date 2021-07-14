using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class GameEventRevealAndPlay : GameEvent
{
    [ShowInInspector]
    private CardManagement.DeckType _revealFromDeckType;

    [ShowInInspector, OdinSerialize]
    private Type _cardTypeToMatch;
    
    public override bool Execute(Villain owner, params Card[] cards)
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
                        owner.SetState(Villain.State.PlayFateCard, new PlayFateCardEventArgs{Card = card, Target = Villain});
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