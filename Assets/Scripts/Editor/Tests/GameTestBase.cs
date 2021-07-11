using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Editor.Tests
{
    public class GameTestBase
    {
        protected Game CreateGame()
        {
            var data = AssetDatabase.LoadAssetAtPath<VillainData>("Assets/ScriptableObjects/Maleficent/Maleficent.asset");
            var game = new Game(new GameSettings(){HandSize = 0, MaxPlayers = 2, Random = new Random(0)}, data, data);
            game.Start();
            return game;
        }
    }
}
