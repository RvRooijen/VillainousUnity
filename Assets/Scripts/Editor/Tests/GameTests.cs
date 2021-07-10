using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

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
        public void GameShouldStart()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Deck.FillHand();
            firstPlayer.Deck.Hand.Count.Should().Be(4);
            firstPlayer.CurrentState.Should().Be(Villain.State.Move);
        }

        [Test, Order(1)]
        public void ShouldNotBeAbleTo_MoveToStartingLocation()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            var lastPlayer = game.Players.Last();
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[0]).Should().BeFalse();
            lastPlayer.Realm.Move(firstPlayer.Realm.Locations[0]).Should().BeFalse();
        }

        [Test, Order(2)]
        public void GainPowerAction_ShouldGivePower()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            var lastPlayer = game.Players.Last();
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventGainPower).Execute();
            
            // Assert
            firstPlayer.Power.Should().Be(2);
            lastPlayer.Power.Should().Be(0);
        }
        
        [Test, Order(2)]
        public void ShouldMoveToBriarRoseCottage()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            var lastPlayer = game.Players.Last();
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.Realm.CurrentLocation.name.Should().Be("Briar Rose's Cottage(Clone)");
            lastPlayer.Realm.CurrentLocation.name.Should().Be("Forbidden Mountains(Clone)");
        }
        
        [Test, Order(2)]
        public void LastCardInHand_ShouldBeSinisterGoon()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Deck.FillHand();
            var hand = firstPlayer.Deck.Hand;
            hand[3].name.Should().Be("Sinister Goon(Clone)");
        }

        [Test, Order(2)]
        public void ShouldNotPlayCard_WhenActionIsNotTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.Deck.FillHand();

            var card = firstPlayer.Deck.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            // Should not be able to play card since play card action is not taken yet
            firstPlayer.PlayVillainCard(card, location);
            
            // Assert
            firstPlayer.Deck.Hand.Count.Should().Be(4);
        }

        [Test, Order(2)]
        public void ShouldBeAbleToPlayCard_WhenPlayerHasEnoughPower_AndPlayCardActionIsTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.Deck.FillHand();

            var card = firstPlayer.Deck.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventGainPower).Execute();
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventPlayCard).Execute();
            firstPlayer.PlayVillainCard(card, firstPlayer.Realm.Locations[1]);
            
            // Assert
            firstPlayer.Deck.Hand.Count.Should().Be(3);
            location.PlacedVillainCards.Should().Contain(card);
        }
        
        [Test, Order(2)]
        public void ShouldFailToPlayCard_WhenPlayerHasNoPower_AndPlayCardActionIsTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.Deck.FillHand();

            Card card = firstPlayer.Deck.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventPlayCard).Execute();
            firstPlayer.PlayVillainCard(card, location);
            
            // Assert
            firstPlayer.Deck.Hand.Count.Should().Be(4);
        }
    }
}
