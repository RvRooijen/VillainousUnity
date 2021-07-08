using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public abstract class Card : UniqueObject
{
    public struct TriggerEvent
    {
        public GameEvent.TriggerType TriggerType;
        public GameEvent GameEvent;
    }
    
    protected Villain Villain;
    
    [ShowInInspector]
    private int _powerCost;

    public List<TriggerEvent> GameEvents = new List<TriggerEvent>();
    
    public void Initialize(Villain villain)
    {
        Villain = villain;
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

    protected virtual void Execute()
    {
        
    }

    private void ExecuteEvents(GameEvent.TriggerType triggerType)
    {
        GameEvents
            .Where(e => e.TriggerType == triggerType)
            .ForEach(e => e.GameEvent.Execute());
        
        Execute();
    }
}
