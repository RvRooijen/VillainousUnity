using UnityEngine;

public class GameEvent
{
    protected Villain Villain;

    public void Initialize(Villain villain)
    {
        Villain = villain;
    }

    public virtual void Execute()
    {
        Debug.Log($"Execute event {GetType()}");
    }
}
