using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

public class Deck
{
    private readonly List<Card> _drawPile;
    private readonly List<Card> _discardPile;
    private readonly Villain _villain;

    public int CardsInDrawPile => _drawPile.Count;
    public int CardsInDiscardPile => _discardPile.Count;
    
    public Deck(Dictionary<Card, int> cards, Villain villain)
    {
        _villain = villain;
        _drawPile = cards.SelectMany(InstantiateCards).ToList();
        _drawPile.ForEach(card => card.Initialize(_villain));
        _discardPile = new List<Card>();
    }
        
    private List<Card> InstantiateCards(KeyValuePair<Card, int> pair)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < pair.Value; i++)
        {
            Card newCard = Object.Instantiate(pair.Key);
            cards.Add(newCard);
        }
        return cards;
    }
        
    public void ShuffleDrawPile()
    {
        _drawPile.Shuffle(_villain.Random);
    }

    public void ShuffleDiscardPile()
    {
        _discardPile.Shuffle(_villain.Random);
    }
    
    public void ShuffleDiscardIntoDeck()
    {
        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();
        ShuffleDrawPile();
    }

    public List<Card> TakeFromDrawPile(int amount)
    {
        var cards = RevealCardsFromDrawPile(amount);
        RemoveFromDrawPile(cards);
        return cards;
    }
    
    public void RemoveFromDrawPile(List<Card> cards)
    {
        _drawPile.RemoveAll(cards.Contains);
    }
    
    public void RemoveFromDiscardPile(List<Card> cards)
    {
        _discardPile.RemoveAll(cards.Contains);
    }
    
    public List<Card> RevealCardsFromDrawPile(int amount)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            if (!_drawPile.Any()) ShuffleDiscardIntoDeck();
            cards.Add(_drawPile.Last());
        }
        return cards;
    }
    
    public List<Card> RevealCardsFromDiscardPile(int amount)
    {
        List<Card> cards = new List<Card>();
        for (int i = 0; i < amount; i++)
        {
            if (!_discardPile.Any()) break;
            cards.Add(_discardPile.Last());
        }
        return cards;
    }

    public void AddCardsToDrawPile(params Card[] cards)
    {
        _drawPile.AddRange(cards);
    }

    public void AddCardsToDiscardPile(params Card[] cards)
    {
        _discardPile.AddRange(cards);
    }
}