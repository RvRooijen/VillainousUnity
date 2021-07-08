using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;

namespace Editor.Tests
{
    public class GameTests
    {
        private Game CreateGame()
        {
            var data = AssetDatabase.LoadAssetAtPath<VillainData>("Assets/ScriptableObjects/Maleficent/Maleficent.asset");
            var game = new Game(new GameSettings(){HandSize = 0, MaxPlayers = 2, Random = new Random(0)}, data, data);
            game.Start();
            return game;
        }
    
        [Test, Order(1)]
        public void Should_CreateGame()
        {
            Game game = CreateGame();
            game.Should().NotBeNull();
            game.Players.Count.Should().Be(2);
        }

        [Test, Order(1)]
        public void FirstPlayer_ShouldStart()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Deck.FillHand();
            firstPlayer.Deck.Hand.Count.Should().Be(4);
            firstPlayer.CurrentState.Should().Be(Villain.State.Move);
        }

        [Test, Order(1)]
        public void FirstPlayer_ShouldFailToMoveToStartLocation()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[0]).Should().BeFalse();
        }
        
        [Test, Order(2)]
        public void FirstPlayer_ShouldMove()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
        }
        
        [Test, Order(2)]
        public void FirstPlayer_ShouldPlayCard()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
        }
    }
}
