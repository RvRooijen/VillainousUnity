using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerAction", fileName = "PlayerAction", order = 0)]
public class PlayerAction : SerializedScriptableObject
{
    public Sprite Sprite;
    public GameEvent GameEvent;
    private Villain _villain;
    public bool Available { get; private set; }

    public void Initialize(Villain villain)
    {
        _villain = villain;
        GameEvent.Initialize(villain);
        Available = true;
    }

    public void Reset()
    {
        Available = true;
    }

    public void Execute()
    {
        if (Available)
        {
            Debug.Log($"Execute action {name}");
            Available = false;
            GameEvent.Execute(_villain);
        }
        else
        {
            Debug.LogWarning("Action already used");
        }
    }
}
