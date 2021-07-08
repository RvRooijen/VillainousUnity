using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Deck
{
    [NonSerialized]
    private Villain _villain;
    public List<Card> Hand;
    
    public List<Card> FateDeck;
    public List<Card> VillainDeck;
    public List<Card> FateDiscard;
    public List<Card> VillainDiscard;

    public enum DeckType
    {
        Fate,
        Villain
    }

    public Deck(VillainData newVillainData, Villain villain)
    {
        _villain = villain;
        FateDiscard = new List<Card>();
        VillainDiscard = new List<Card>();
        Hand = new List<Card>();

        FateDeck = newVillainData.FateDeck.SelectMany(InstantiateCards).ToList();
        VillainDeck = newVillainData.VillainDeck.SelectMany(InstantiateCards).ToList();
    }
    
    private List<Card> InstantiateCards(KeyValuePair<Card, int> pair)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < pair.Value; i++)
        {
            var newCard = Object.Instantiate(pair.Key);
            cards.Add(newCard);
            newCard.Initialize(_villain);
        }
        return cards;
    }

    public void Shuffle()
    {
        VillainDeck.Shuffle(_villain.Random);
        FateDeck.Shuffle(_villain.Random);
    }

    public Card Discard(int index, List<Card> from, List<Card> to)
    {
        if (from.Count > index)
        {
            var card = from[index];
            to.Add(card);
            from.RemoveAt(index);
            return card;
        }

        Debug.LogWarning($"No card found in from on index {index}");
        return null;
    }

    public void FillHand()
    {
        AddToHand(GetCards(VillainDeck, EmptyHandSpots()));
    }
    
    public void AddToHand(List<Card> cards)
    {
        Hand.AddRange(cards);
    }

    public List<Card> GetCards(List<Card> deck, int amount)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            if (!deck.Any()) break;
            cards.Add(deck.Last());
            deck.RemoveAt(deck.Count-1);
        }
        return cards;
    }

    public int EmptyHandSpots()
    {
        return 4 - Hand.Count;
    }
}