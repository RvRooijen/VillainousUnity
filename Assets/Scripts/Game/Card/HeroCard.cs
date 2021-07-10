using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create HeroCard", fileName = "HeroCard", order = 0)]
public class HeroCard : Card
{
    [ShowInInspector]
    private int _strength;
}