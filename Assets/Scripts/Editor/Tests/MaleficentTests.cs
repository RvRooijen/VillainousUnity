using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class MaleficentTests : CardTests
    {   
        [Test]
        public void AuroraShouldFate_WhenHeroIsPulled()
        {
            // Create and check
            Game game = CreateGame();
            var path = "Assets/ScriptableObjects/Maleficent/FateDeck/Aurora.asset";
            var card = CreateCard(path, game, out var firstPlayer, out var otherPlayer, out var hand);
            card.Initialize(otherPlayer);
            
            card.GameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.GameEvents.ForEach(e => e.GameEvent.Initialize(otherPlayer));
            
            // Add cards to top | hero - non hero - Aurora |
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => c is HeroCard);
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => !(c is HeroCard));
            otherPlayer.cardManagement.FateDeck.AddCard(card);
            
            // Move and take the fate action
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[3]).Should().BeTrue();
            
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventFate).Execute();
            firstPlayer.CurrentState.Should().Be(Villain.State.ChooseFateTarget);
            
            firstPlayer.SetState(Villain.State.PickFateCard, new PickFateCardEventArgs {Target = otherPlayer});
            firstPlayer.CurrentState.Should().Be(Villain.State.PickFateCard);
            
            var fateTargetLocation = otherPlayer.Realm.Locations.First();
            var possibleCards = otherPlayer.GetFateOptions();

            possibleCards.First().Should().Be(card);
            
            firstPlayer.SetState(Villain.State.PlayFateCard, new PlayFateCardEventArgs {Target = otherPlayer, Card = possibleCards.First()});
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);
            
            otherPlayer.Fate(possibleCards, possibleCards.First(), fateTargetLocation, firstPlayer);
            
            // Aurora should have triggered
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);

            otherPlayer.Realm.Locations.First().PlacedFateCards.Count.Should().Be(1);
            
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }

        [Test]
        public void Card_Guards()
        {
            
        }
    }
}