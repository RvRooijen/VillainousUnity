using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VillainAI
{
    private Villain _villain;

    public VillainAI(Villain villain)
    {
        _villain = villain;
        villain.StateChangedEvents[Villain.State.Move] += EnterMoveState;
        villain.StateChangedEvents[Villain.State.SelectAction] += EnterPerformActionsState;
        villain.StateChangedEvents[Villain.State.Discarding] += EnterDiscardingState;
        villain.StateChangedEvents[Villain.State.PlayCard] += EnterPlayCardState;
        villain.StateChangedEvents[Villain.State.MoveItemAlly] += EnterMoveItemAllyState;
    }

    private void EnterMoveState(object sender, EventArgs e)
    {
        Debug.Log($"{nameof(VillainAI)} - {nameof(EnterMoveState)}");
        _villain.Realm.Move(GetRandomLocation());
    }

    private Location GetRandomLocation()
    {
        var locations = _villain.Realm.Locations.ToList();
        locations.Remove(_villain.Realm.CurrentLocation);
        locations.Shuffle();
        return locations.First();
    }

    private void EnterPerformActionsState(object sender, EventArgs e)
    {
        Debug.Log($"{nameof(VillainAI)} - {nameof(EnterPerformActionsState)}");
        var availableAction = _villain.Realm.CurrentLocation.PlayerActions.FirstOrDefault(action => action.Available);
        if (availableAction != null)
        {
            availableAction.Execute();
        }
        else
        {
            _villain.EndTurn();
        }
    }
    
    private void EnterDiscardingState(object sender, EventArgs e)
    {
        Debug.Log($"{nameof(VillainAI)} - {nameof(EnterDiscardingState)}");
        while (_villain.Deck.Hand.Any())
        {
            var discarded = _villain.Deck.Discard(0, _villain.Deck.Hand, _villain.Deck.VillainDiscard);
            Debug.Log($"Discarding {discarded.name}");
        }

        _villain.CurrentState = Villain.State.SelectAction;
    }
    
    private void EnterPlayCardState(object sender, EventArgs e)
    {
        _villain.CurrentState = Villain.State.SelectAction;
    }
    
    private void EnterMoveItemAllyState(object sender, EventArgs e)
    {
        _villain.CurrentState = Villain.State.SelectAction;
    }
}