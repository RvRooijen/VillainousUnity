using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Villain
{
    [ShowInInspector]
    private int _power = 0;
    public Deck Deck;
    public Realm Realm;

    private VillainAI _ai;
    
    public enum State
    {
        CheckWin,
        Move,
        SelectAction,
        Discarding,
        PlayCard,
        MoveItemAlly,
        Inactive,
        Won,
        Lost
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
        Deck = new Deck(newVillainData, this);
        Deck.Shuffle();

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
        
        Deck.FillHand();

        _ai = new VillainAI(this);
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
    
    public virtual void EndTurn()
    {
        Debug.Log($"{GetType()} {nameof(EndTurn)}");
        Deck.FillHand();
        CurrentState = State.Inactive;
    }

    public void IncreasePower(int value)
    {
        _power += value;
    }
}
