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
}

[Serializable]
public class Game
{
    public List<Villain> Players;
    
    public Game(GameSettings gameSettings, params VillainData[] players)
    {
        Players = new List<Villain>();
        
        foreach (VillainData villain in players)
        {
            VillainData newVillainData = Object.Instantiate(villain);
            object[] p = { gameSettings, newVillainData };
            Villain newVillain = Activator.CreateInstance(newVillainData.BehaviorType, p) as Villain;
            Players.Add(newVillain);
        }
        
        Players.First().StartTurn();
    }
}
