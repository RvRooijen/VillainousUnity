using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public VillainData P1;
    public VillainData P2;

    public Game Game;
    
    void Start()
    {
        Game = new Game(new GameSettings()
        {
            HandSize = 4,
            MaxPlayers = 0
        }, P1, P2);
    }
}
