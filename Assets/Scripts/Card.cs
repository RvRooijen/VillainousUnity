using System.Collections.Generic;
using Sirenix.OdinInspector;

public abstract class Card : UniqueObject
{
    protected Villain Villain;
    
    [ShowInInspector]
    private int _powerCost;
    public List<GameEvent> PlayedEvents = new List<GameEvent>(); 
    public List<GameEvent> DiscardEvents = new List<GameEvent>(); 
    
    public void Initialize(Villain villain)
    {
        Villain = villain;
    }
    
    public abstract void Play();
    public abstract void Discard();
    public abstract void Execute();
}
