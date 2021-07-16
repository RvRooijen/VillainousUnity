using System;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class GameEventRequiresMultipleOfType : GameEvent
{
    [ShowInInspector]
    private int Amount;
    
    [ShowInInspector, OdinSerialize]
    private Type _cardTypeToMatch;
    
    public override bool Execute(Villain origin, params Card[] cards)
    {
        return cards.Count(card => card.GetType() == _cardTypeToMatch) >= Amount;
    }
}