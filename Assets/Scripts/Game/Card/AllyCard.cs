using UnityEngine;

[CreateAssetMenu(menuName = "Create AllyCard", fileName = "AllyCard", order = 0)]
public class AllyCard : Card
{
    [SerializeField]
    public int Strength { get; private set; }
}