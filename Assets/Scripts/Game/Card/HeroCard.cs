using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create HeroCard", fileName = "HeroCard", order = 0)]
public class HeroCard : Card
{
    [ShowInInspector]
    private int _strength;
    
    public override void Play()
    {
        throw new System.NotImplementedException();
    }

    public override void Discard()
    {
        throw new System.NotImplementedException();
    }

    protected override void Execute()
    {
        throw new System.NotImplementedException();
    }
}