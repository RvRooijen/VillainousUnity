using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

public struct GameSettings
{
    public int HandSize;
    public int MaxPlayers;
    public Random Random;
}

[Serializable]
public class Game
{
    public List<Villain> Players;
    
    public Game(GameSettings gameSettings, params VillainData[] players)
    {
        Players = new List<Villain>();
        
        foreach (VillainData villainData in players)
        {
            VillainData newVillainData = Object.Instantiate(villainData);
            object[] p = { gameSettings, newVillainData };
            Villain newVillain = Activator.CreateInstance(newVillainData.BehaviorType, p) as Villain;
            Players.Add(newVillain);
        }
    }

    public void Start()
    {
        Players.First()
            .StartTurn();
    }
}
