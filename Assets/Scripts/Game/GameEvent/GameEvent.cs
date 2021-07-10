using UnityEngine;

public class GameEvent
{
    public enum TriggerType
    {
        OnCanPlayOtherCard,
        OnPlay,
        OnActivate,
        OnDiscard
    }
    
    protected Villain Villain;

    public void Initialize(Villain villain)
    {
        Villain = villain;
    }

    public virtual bool Execute(params Card[] cards)
    {
        Debug.Log($"Execute event {GetType()}");
        return true;
    }
}
