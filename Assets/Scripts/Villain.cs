using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Object = UnityEngine.Object;

[Serializable]
public class Villain
{
    [ShowInInspector]
    private int _power = 0;
    public Deck Deck;
    public Realm Realm;
    
    public enum State
    {
        CheckWin,
        Move,
        PerformActions,
        Inactive,
        Won,
        Lost
    }

    [ShowInInspector]
    private State _currentState = State.Inactive;

    private State CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            StateChangedEvents[value]?.Invoke(this, EventArgs.Empty);
        }
    }

    public Dictionary<State, EventHandler> StateChangedEvents = new Dictionary<State, EventHandler>();
    
    protected Villain(GameSettings gameSettings, VillainData newVillainData)
    {
        Deck = new Deck(newVillainData);
        Deck.Shuffle();

        Realm = Object.Instantiate(newVillainData.Realm);
        Realm.Initialize();
        
        StateChangedEvents.Add(State.CheckWin, EnterCheckWinState);
        StateChangedEvents.Add(State.Move, EnterMoveState);
        StateChangedEvents.Add(State.Won, EnterWonState);
        StateChangedEvents.Add(State.Lost, EnterLostState);
        StateChangedEvents.Add(State.PerformActions, EnterPerformActionsState);
        
        Deck.FillHand();
    }

    public virtual void StartTurn()
    {
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
    
    public virtual void EnterWonState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EnterLostState(object sender, EventArgs e)
    {
        
    }
    
    public virtual void EndTurn()
    {
        Deck.FillHand();
        CurrentState = State.Inactive;
    }

    public void IncreasePower(int value)
    {
        _power += value;
    }
}
