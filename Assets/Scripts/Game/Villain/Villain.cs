using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class PickFateCardEventArgs : EventArgs
{
    public Villain Target;
}

public class PlayFateCardEventArgs : EventArgs
{
    public Villain Target;
    public Card Card;
}
public class PlaceCardEventArgs : EventArgs
{
    public Card Card;
}

[Serializable]
public class Villain
{
    public Random Random;
    
    [ShowInInspector]
    private int _power = 0;
    public int Power => _power;
    public State CurrentState => _currentState;

    public CardManagement cardManagement;
    public Realm Realm;

    private VillainController _controller;
    
    public enum State
    {
        CheckWin,
        Move,
        SelectAction,
        Discarding,
        PlayCard,
        PickMoveItemAlly,
        MoveItemAlly,
        ChooseFateTarget,
        ChooseVanquishTarget,
        PickFateCard,
        PlayFateCard,
        PlaceCard,
        Inactive,
        Won,
        Lost
    }

    public enum PlayCardFailType
    {
        None,
        InsufficientPower,
        NotInHand,
        BlockedByCard,
        IncorrectState
    }
    
    [ShowInInspector]
    private State _currentState = State.Inactive;

    public void SetState(State state, EventArgs args)
    {
        if (_currentState == state) return;
        _currentState = state;
        if (StateChangedEvents.ContainsKey(state))
        {
            Debug.Log($"{nameof(SetState)} Enter state {state}");
            StateChangedEvents[state]?.Invoke(this, args);
        }
    }

    public Dictionary<State, EventHandler<EventArgs>> StateChangedEvents = new Dictionary<State, EventHandler<EventArgs>>();
    
    protected Villain(GameSettings gameSettings, VillainData newVillainData)
    {
        Random = gameSettings.Random;
        cardManagement = new CardManagement(newVillainData, this);
        cardManagement.VillainDeck.ShuffleDrawPile();
        cardManagement.FateDeck.ShuffleDrawPile();

        Realm = Object.Instantiate(newVillainData.Realm);
        Realm.Initialize(this);
        
        StateChangedEvents.Add(State.CheckWin, EnterCheckWinState);
        StateChangedEvents.Add(State.Move, EnterMoveState);
        StateChangedEvents.Add(State.Won, EnterWonState);
        StateChangedEvents.Add(State.Lost, EnterLostState);
        StateChangedEvents.Add(State.SelectAction, EnterPerformActionsState);
        StateChangedEvents.Add(State.Discarding, EnterPerformActionsDiscard);
        StateChangedEvents.Add(State.PlayCard, EnterPlayCardState);
        StateChangedEvents.Add(State.PickMoveItemAlly, EnterPickMoveItemAlly);
        StateChangedEvents.Add(State.MoveItemAlly, EnterMoveItemAllyState);
        StateChangedEvents.Add(State.ChooseFateTarget, EnterChooseFateTargetState);
        StateChangedEvents.Add(State.ChooseVanquishTarget, EnterChooseVanquishTargetState);
        StateChangedEvents.Add(State.PickFateCard, EnterPickFateCardState);
        StateChangedEvents.Add(State.PlayFateCard, EnterPlayFateCardState);
        StateChangedEvents.Add(State.PlaceCard, EnterPlaceCardState);
    }

    private void EnterChooseVanquishTargetState(object sender, EventArgs e)
    {
        
    }

    protected virtual void EnterPlaceCardState(object sender, EventArgs e)
    {
        
    }

    protected virtual void EnterPlayFateCardState(object sender, EventArgs e)
    {
        if (e is PlayFateCardEventArgs args)
        {
            Debug.Log($"{nameof(EnterPlayFateCardState)} card {args.Card} target {args.Target}");
        }
        else
        {
            Debug.LogWarning($"{nameof(EnterPlayFateCardState)} wrong arg types {e.GetType()}");
        }
    }

    protected virtual void EnterChooseFateTargetState(object sender, EventArgs e)
    {
        
    }

    protected virtual void EnterPickMoveItemAlly(object sender, EventArgs e)
    {
        
    }

    public virtual void StartTurn()
    {
        Debug.Log($"{GetType()} {nameof(StartTurn)}");
        Realm.Locations.ForEach(location => location.PlayerActions.ForEach(action => action.Reset()));
        SetState(State.CheckWin, EventArgs.Empty);
    }

    protected virtual void EnterCheckWinState(object sender, EventArgs e)
    {
        SetState(State.Move, EventArgs.Empty);
    }

    protected virtual void EnterMoveState(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterPerformActionsState(object sender, EventArgs e)
    {
        
    }
    protected virtual void EnterPerformActionsDiscard(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterPlayCardState(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterMoveItemAllyState(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterWonState(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterLostState(object sender, EventArgs e)
    {
        
    }
    
    protected virtual void EnterPickFateCardState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EndTurn()
    {
        Debug.Log($"{GetType()} {nameof(EndTurn)}");
        cardManagement.FillHand();
        SetState(State.Inactive, EventArgs.Empty);
    }

    public void IncreasePower(int value)
    {
        _power += value;
    }

    public bool PayPower(int cost)
    {
        if (cost > _power) return false;
        _power -= cost;
        return true;
    }
    
    public void PlayVillainCard(Card card, Location location)
    {
        if (!CanPlayCard(card, location, out var reason))
        {
            Debug.Log(reason);
            return;
        }

        cardManagement.Hand.Remove(card);
        location.PlacedVillainCards.Add(card);
        
        card.Play(this);
        
        Debug.Log($"Played {card} to {location}");
    }

    private bool CanPlayCard(Card card, Location location, out PlayCardFailType failType)
    {
        if (!IsInCorrectState(State.PlayCard))
        {
            failType = PlayCardFailType.IncorrectState;
            return false;
        }

        if (!cardManagement.Hand.Contains(card))
        {
            failType = PlayCardFailType.NotInHand;
            return false;
        }

        if (!IsAllowedByOtherCards(card, location))
        {
            failType = PlayCardFailType.BlockedByCard;
            return false;
        }

        if (!PayPower(card.PowerCost))
        {
            failType = PlayCardFailType.InsufficientPower;
            return false;
        }
        
        failType = PlayCardFailType.None;
        return true;
    }

    private bool IsAllowedByOtherCards(Card card, Location location)
    {
        foreach (Card placedCard in location.PlacedVillainCards)
        {
            if (placedCard.OnCanPlayOtherCard(card)) continue;
            Debug.Log($"Can't play card {card} because of {placedCard}");
            return false;
        }

        return true;
    }

    public bool IsInCorrectState(State state)
    {
        if (state == _currentState)
        {
            return true;
        }

        Debug.LogWarning($"Need to be in {state}. Current state {_currentState}");
        return false;
    }

    public List<Card> GetFateOptions()
    {
        return cardManagement.FateDeck.RevealCardsFromDrawPile(2);
    }

    public void Fate(List<Card> options, Card pickedOption, ITargetable targetable, Villain attacker)
    {
        Debug.Log($"{nameof(Fate)} picked {pickedOption} target {targetable} attacker {attacker}");
        if (!attacker.IsInCorrectState(State.PlayFateCard)) return;
        if (!IsInCorrectState(State.Inactive)) return;
        if (!options.Contains(pickedOption))
        {
            Debug.LogError($"Picked option is not available {pickedOption}");
            return;
        }
        
        attacker.SetState(State.SelectAction, EventArgs.Empty);

        cardManagement.FateDeck.AddCardsToDiscardPile(options.Where(card => card != pickedOption).ToArray());
        cardManagement.FateDeck.RemoveFromDrawPile(options);
        
        targetable.AddFateCard(attacker, pickedOption);
        pickedOption.Play(attacker);
    }
}
