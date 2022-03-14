using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

/// <summary>
/// Should contain cards of type to a minimal of amount
/// </summary>
public class GameEventShouldContainCards : GameEvent
{
    [SerializeField]
    private int _amount;
    
    [ShowInInspector, OdinSerialize]
    private Type _cardTypeToMatch;
    
    public override bool Execute(Villain origin, params Card[] cards)
    {
        // Should contain cards of type to a minimal of amount
        return cards.Count(card => card.GetType() == _cardTypeToMatch) >= _amount;
    }
}