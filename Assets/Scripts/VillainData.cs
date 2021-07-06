using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Villain data", fileName = "Villain data", order = 0)]
public class VillainData : SerializedScriptableObject
{
    public Realm Realm;
    public Dictionary<Card, int> FateDeck = new Dictionary<Card, int>();
    public Dictionary<Card, int> VillainDeck = new Dictionary<Card, int>();
    public Type BehaviorType;
}