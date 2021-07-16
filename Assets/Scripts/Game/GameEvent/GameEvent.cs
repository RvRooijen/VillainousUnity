using UnityEngine;

public class GameEvent
{
    public enum TriggerType
    {
        OnCanPlayOtherCard,
        OnPlay,
        OnActivate,
        OnDiscard,
        OnTarget,
        OnIsValidTarget,
    }
    
    protected Villain Villain;

    public void Initialize(Villain villain)
    {
        Villain = villain;
    }

    public virtual bool Execute(Villain origin, params Card[] cards)
    {
        Debug.Log($"Execute event {GetType()}");
        return true;
    }
}
