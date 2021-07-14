using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public abstract class Card : UniqueObject, ITargetable
{
    public struct TriggerEvent
    {
        public GameEvent.TriggerType TriggerType;
        public GameEvent GameEvent;
    }
    
    protected Villain Villain;
    public int PowerCost;

    public List<TriggerEvent> GameEvents = new List<TriggerEvent>();
    public List<Card> AttachedCards = new List<Card>();
    
    public void Initialize(Villain villain)
    {
        Villain = villain;
    }

    public virtual bool OnCanPlayOtherCard(Card card)
    {
        return ExecuteEvents(GameEvent.TriggerType.OnCanPlayOtherCard, card);
    }
    
    public virtual void Play()
    {
        ExecuteEvents(GameEvent.TriggerType.OnPlay);
    }

    public virtual void Discard()
    {
        ExecuteEvents(GameEvent.TriggerType.OnDiscard);
    }

    public virtual void Activate()
    {
        ExecuteEvents(GameEvent.TriggerType.OnActivate);
    }

    private bool ExecuteEvents(GameEvent.TriggerType triggerType, params Card[] cards)
    {
        return GameEvents
            .Where(e => e.TriggerType == triggerType)
            .All(e => e.GameEvent.Execute(Villain, cards));
    }

    public void Target(Card card)
    {
        AttachedCards.Add(card);
    }
}
