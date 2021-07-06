using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create Location", fileName = "Location", order = 0)]
public class Location : SerializedScriptableObject
{
    public List<PlayerAction> PlayerActions = new List<PlayerAction>();
    
    public void Initialize()
    {
        PlayerActions = PlayerActions.Select(Instantiate).ToList();
    }
}
