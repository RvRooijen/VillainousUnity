﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Editor.Tests
{
    public class CardManagementTests : GameTestBase
    {
        [Test]
        public void FateOpponent()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            var otherPlayer = game.Players.Last();
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[3]).Should().BeTrue();
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventFate).Execute();
            firstPlayer.CurrentState.Should().Be(Villain.State.ChooseFateTarget);
            
            firstPlayer.SetState(Villain.State.PickFateCard, new PickFateCardEventArgs {Target = otherPlayer});
            firstPlayer.CurrentState.Should().Be(Villain.State.PickFateCard);
            
            var fateTargetLocation = otherPlayer.Realm.Locations.First();
            var possibleCards = otherPlayer.GetFateOptions(2);
            
            firstPlayer.SetState(Villain.State.PlayFateCard, new PlayFateCardEventArgs {Target = otherPlayer, Card = possibleCards.First()});
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);
            
            otherPlayer.Fate(possibleCards, possibleCards.First(), fateTargetLocation, firstPlayer);

            fateTargetLocation.PlacedFateCards.Count.Should().Be(1);
        }
    }
}