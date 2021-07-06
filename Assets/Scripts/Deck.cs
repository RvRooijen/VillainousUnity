using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Deck
{
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

    public Deck(VillainData newVillainData)
    {
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
            cards.Add(Object.Instantiate(pair.Key));
        }
        return cards;
    }

    public void Shuffle()
    {
        VillainDeck.Shuffle();
        FateDeck.Shuffle();
    }

    public void Discard(int index, List<Card> discard)
    {
        if (Hand.Count > index)
        {
            discard.Add(Hand[index]);
            Hand.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning($"No card found in hand on index {index}");
        }
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
            cards.RemoveAt(0);
        }
        return cards;
    }

    public int EmptyHandSpots()
    {
        return 4 - Hand.Count;
    }
}