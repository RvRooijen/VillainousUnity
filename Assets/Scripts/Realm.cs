using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Realm", fileName = "Realm", order = 0)]
public class Realm : SerializedScriptableObject
{
    public List<Location> Locations = new List<Location>();

    public void Initialize()
    {
        Locations = Locations.Select(Instantiate).ToList();
        Locations.ForEach(location => location.Initialize());
    }
}
