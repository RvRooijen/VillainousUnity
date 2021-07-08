using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create Location", fileName = "Location", order = 0)]
public class Location : SerializedScriptableObject
{
    public List<PlayerAction> PlayerActions;
    public List<Card> PlacedCards;
    
    public void CanPlayCard(Card card)
    {
        
    }

    public void Initialize(Villain villain)
    {
        PlacedCards = new List<Card>();
        PlayerActions.ForEach(action => action.Initialize(villain));
    }
}
