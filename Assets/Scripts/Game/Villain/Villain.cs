using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

[Serializable]
public class Villain
{
    public Random Random;
    
    [ShowInInspector]
    private int _power = 0;
    public int Power => _power;
    
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
        MoveItemAlly,
        Fate,
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

    public State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            if(StateChangedEvents.ContainsKey(value))
                StateChangedEvents[value]?.Invoke(this, EventArgs.Empty);
        }
    }

    public Dictionary<State, EventHandler> StateChangedEvents = new Dictionary<State, EventHandler>();
    
    protected Villain(GameSettings gameSettings, VillainData newVillainData)
    {
        Random = gameSettings.Random;
        cardManagement = new CardManagement(newVillainData, this);
        cardManagement.VillainDeck.ShuffleDrawPile();

        Realm = Object.Instantiate(newVillainData.Realm);
        Realm.Initialize(this);
        
        StateChangedEvents.Add(State.CheckWin, EnterCheckWinState);
        StateChangedEvents.Add(State.Move, EnterMoveState);
        StateChangedEvents.Add(State.Won, EnterWonState);
        StateChangedEvents.Add(State.Lost, EnterLostState);
        StateChangedEvents.Add(State.SelectAction, EnterPerformActionsState);
        StateChangedEvents.Add(State.Discarding, EnterPerformActionsDiscard);
        StateChangedEvents.Add(State.PlayCard, EnterPlayCardState);
        StateChangedEvents.Add(State.MoveItemAlly, EnterMoveItemAllyState);
        StateChangedEvents.Add(State.Fate, EnterFateState);
    }

    public virtual void StartTurn()
    {
        Debug.Log($"{GetType()} {nameof(StartTurn)}");
        Realm.Locations.ForEach(location => location.PlayerActions.ForEach(action => action.Reset()));
        CurrentState = State.CheckWin;
    }

    public virtual void EnterCheckWinState(object sender, EventArgs e)
    {
        CurrentState = State.Move;
    }

    public virtual void EnterMoveState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EnterPerformActionsState(object sender, EventArgs e)
    {
        
    }
    public virtual void EnterPerformActionsDiscard(object sender, EventArgs e)
    {
        
    }
    
    private void EnterPlayCardState(object sender, EventArgs e)
    {
        
    }
    
    private void EnterMoveItemAllyState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EnterWonState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EnterLostState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EnterFateState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EndTurn()
    {
        Debug.Log($"{GetType()} {nameof(EndTurn)}");
        cardManagement.FillHand();
        CurrentState = State.Inactive;
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
        
        card.Play();
        
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

        if (!IsAllowedByOtherCard(card, location))
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

    private bool IsAllowedByOtherCard(Card card, Location location)
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
        if (state == CurrentState)
        {
            return true;
        }

        Debug.LogWarning($"Need to be in {state}. Current state {CurrentState}");
        return false;
    }

    public List<Card> GetFateOptions()
    {
        return cardManagement.FateDeck.GetCardsFromDrawPile(2, true);
    }
}
