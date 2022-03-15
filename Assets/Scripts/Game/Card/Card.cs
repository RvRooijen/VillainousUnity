using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public abstract class Card : UniqueObject, ITargetable
{
    public string Title;
    [TextArea]
    public string Description;
    
    public struct TriggerEvent
    {
        public GameEvent.TriggerType TriggerType;
        public GameEvent GameEvent;
    }

    public Villain Villain { get; private set; }
    public int PowerCost;

    public List<TriggerEvent> GameEvents = new List<TriggerEvent>();
    public List<Card> AttachedCards = new List<Card>();

    public void Initialize(Villain villain)
    {
        Villain = villain;
    }

    public virtual bool OnCanPlayOtherCard(Card card)
    {
        return ExecuteEvents(GameEvent.TriggerType.OnCanPlayOtherCard, Villain, card);
    }
    
    public virtual void Play(Villain origin)
    {
        ExecuteEvents(GameEvent.TriggerType.OnPlay, origin);
    }

    public virtual void Discard(Villain origin)
    {
        ExecuteEvents(GameEvent.TriggerType.OnDiscard, origin);
    }

    public virtual void Activate(Villain origin)
    {
        ExecuteEvents(GameEvent.TriggerType.OnActivate, origin);
    }

    private bool ExecuteEvents(GameEvent.TriggerType triggerType, Villain origin, params Card[] cards)
    {
        return GameEvents
            .Where(e => e.TriggerType == triggerType)
            .All(e => e.GameEvent.Execute(origin, cards));
    }

    public virtual bool IsValidTarget(params Card[] cards)
    {
        return ExecuteEvents(GameEvent.TriggerType.OnIsValidTarget, Villain, cards);
    }
    
    /// <summary>
    /// A card is targeting this card
    /// </summary>
    /// <param name="cards"></param>
    public virtual void AddFateCard(Villain origin, params Card[] cards)
    {
        cards.ForEach(card => card.ExecuteEvents(GameEvent.TriggerType.OnTarget, origin, this));
    }

    public void OnDestroyed(Villain origin)
    {
        ExecuteEvents(GameEvent.TriggerType.OnDestroyed, origin);
    }
}
