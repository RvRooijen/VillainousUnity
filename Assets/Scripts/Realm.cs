using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Realm", fileName = "Realm", order = 0)]
public class Realm : SerializedScriptableObject
{
    public List<Location> Locations = new List<Location>();
    public Location CurrentLocation;
    private Villain _villain;

    public void Initialize(Villain villain)
    {
        _villain = villain;
        Locations = Locations.Select(Instantiate).ToList();
        Locations.ForEach(location => location.Initialize(_villain));
        CurrentLocation = Locations.First();
    }

    public void Move(Location location)
    {
        if (_villain.CurrentState == Villain.State.Move)
        {
            Debug.Log($"Move from {CurrentLocation.name} to {location.name}");
            CurrentLocation = location;
            _villain.CurrentState = Villain.State.SelectAction;
        }
        else
        {
            Debug.LogWarning($"Villain tries to move but is not in move state");
        }
    }
}
