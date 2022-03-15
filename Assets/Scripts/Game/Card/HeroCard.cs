using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create HeroCard", fileName = "HeroCard", order = 0)]
public class HeroCard : Card
{
    [SerializeField]
    public int Strength { get; private set; }
}