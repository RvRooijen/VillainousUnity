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
    public List<Card> PlacedVillainCards;
    public List<Card> PlacedFateCards;
    
    public void PlayVillainCard(Card card)
    {
        PlacedVillainCards.Add(card);
    }

    public void PlayFateCard(Card card)
    {
        PlacedFateCards.Add(card);
    }
    
    public void Initialize(Villain villain)
    {
        PlacedVillainCards = new List<Card>();
        PlayerActions = PlayerActions.Select(Instantiate).ToList();
        PlayerActions.ForEach(action => action.Initialize(villain));
    }
}
