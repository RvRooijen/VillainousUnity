using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerAction", fileName = "PlayerAction", order = 0)]
public class PlayerAction : SerializedScriptableObject
{
    public Sprite Sprite;
    public GameEvent GameEvent;
}
