using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CardManagement
{
    [NonSerialized]
    private Villain _villain;
    public List<Card> Hand;
    
    public Deck VillainDeck;
    public Deck FateDeck;

    public enum DeckType
    {
        Fate,
        Villain
    }

    public CardManagement(VillainData villainData, Villain villain)
    {
        _villain = villain;
        VillainDeck = new Deck(villainData.VillainDeck, villain);
        FateDeck = new Deck(villainData.FateDeck, villain);
        Hand = new List<Card>();
    }
    
    public void DiscardFromHand(Card card)
    {
        if (!Hand.Contains(card))
        {
            Debug.LogWarning($"Card not in hand {card}");
            return;
        }
        
        VillainDeck.AddCardsToDiscardPile(card);
    }

    public void FillHand()
    {
        AddToHand(VillainDeck.GetCardsFromDrawPile(EmptyHandSpots(), true));
    }
    
    public void AddToHand(List<Card> cards)
    {
        Hand.AddRange(cards);
    }

    public int EmptyHandSpots()
    {
        return 4 - Hand.Count;
    }
}