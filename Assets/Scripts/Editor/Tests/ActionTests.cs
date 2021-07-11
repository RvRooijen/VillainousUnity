using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Editor.Tests
{
    public class ActionTests : GameTestBase
    {
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
        public void ShouldNotPlayCard_WhenActionIsNotTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.cardManagement.FillHand();

            var card = firstPlayer.cardManagement.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            // Should not be able to play card since play card action is not taken yet
            firstPlayer.PlayVillainCard(card, location);
            
            // Assert
            firstPlayer.cardManagement.Hand.Count.Should().Be(4);
        }

        [Test, Order(2)]
        public void ShouldBeAbleToPlayCard_WhenPlayerHasEnoughPower_AndPlayCardActionIsTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.cardManagement.FillHand();

            var card = firstPlayer.cardManagement.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventGainPower).Execute();
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventPlayCard).Execute();
            firstPlayer.PlayVillainCard(card, firstPlayer.Realm.Locations[1]);
            
            // Assert
            firstPlayer.cardManagement.Hand.Count.Should().Be(3);
            location.PlacedVillainCards.Should().Contain(card);
        }
        
        [Test, Order(2)]
        public void ShouldFailToPlayCard_WhenPlayerHasNoPower_AndPlayCardActionIsTaken()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[1]).Should().BeTrue();
            firstPlayer.cardManagement.FillHand();

            Card card = firstPlayer.cardManagement.Hand[3];
            var location = firstPlayer.Realm.Locations[1];
            
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventPlayCard).Execute();
            firstPlayer.PlayVillainCard(card, location);
            
            // Assert
            firstPlayer.cardManagement.Hand.Count.Should().Be(4);
            firstPlayer.cardManagement.Hand.Should().Contain(card);
        }
    }
}