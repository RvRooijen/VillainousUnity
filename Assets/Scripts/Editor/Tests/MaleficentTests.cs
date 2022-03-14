using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class MaleficentTests : CardTests
    {   
        private void MoveAndFate(Villain firstPlayer, Villain otherPlayer, Card card)
        {
            // Move and take the fate action
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[3]).Should().BeTrue();

            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventFate).Execute();
            firstPlayer.CurrentState.Should().Be(Villain.State.ChooseFateTarget);

            firstPlayer.SetState(Villain.State.PickFateCard, new PickFateCardEventArgs {Target = otherPlayer});
            firstPlayer.CurrentState.Should().Be(Villain.State.PickFateCard);

            var fateTargetLocation = otherPlayer.Realm.Locations.First();
            var possibleCards = otherPlayer.GetFateOptions();

            possibleCards.First().Should().Be(card);

            firstPlayer.SetState(Villain.State.PlayFateCard,
                new PlayFateCardEventArgs {Target = otherPlayer, Card = possibleCards.First()});
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);

            otherPlayer.Fate(possibleCards, possibleCards.First(), fateTargetLocation, firstPlayer);
        }
        
        [Test]
        public void AuroraShouldPlayHero_WhenHeroIsPulled()
        {
            // Create and check
            Game game = CreateGame();
            var path = "Assets/ScriptableObjects/Maleficent/FateDeck/Aurora.asset";
            var card = CreateCard(path, game, out var firstPlayer, out var otherPlayer, out var hand, true);
            card.Initialize(otherPlayer);
            
            card.GameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.GameEvents.ForEach(e => e.GameEvent.Initialize(otherPlayer));
            
            // Add cards to top | hero - non hero - Aurora |
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => c is HeroCard);
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => !(c is HeroCard));
            otherPlayer.cardManagement.FateDeck.AddCard(card);
            
            MoveAndFate(firstPlayer, otherPlayer, card);

            // Aurora should have triggered
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);

            otherPlayer.Realm.Locations.First().PlacedFateCards.Count.Should().Be(1);
            
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }

        [Test]
        public void AuroraShouldNotPlayHero_WhenNoHeroIsPulled()
        {
            // Create and check
            Game game = CreateGame();
            var path = "Assets/ScriptableObjects/Maleficent/FateDeck/Aurora.asset";
            var card = CreateCard(path, game, out var firstPlayer, out var otherPlayer, out var hand, true);
            card.Initialize(otherPlayer);
            
            card.GameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.GameEvents.ForEach(e => e.GameEvent.Initialize(otherPlayer));
            
            // Add cards to top | hero - non hero - Aurora |
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => !(c is HeroCard));
            otherPlayer.cardManagement.FateDeck.PutOnTop(c => c is HeroCard);
            otherPlayer.cardManagement.FateDeck.AddCard(card);
            
            MoveAndFate(firstPlayer, otherPlayer, card);
            
            // Aurora should not have triggered
            firstPlayer.CurrentState.Should().Be(Villain.State.SelectAction);

            otherPlayer.Realm.Locations.First().PlacedFateCards.Count.Should().Be(1);
            
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }

        [Test]
        public void Card_Guards()
        {
            // Create and check
            Game game = CreateGame();
            var path = "Assets/ScriptableObjects/Maleficent/FateDeck/Guards.asset";
            var card = CreateCard(path, game, out var firstPlayer, out var otherPlayer, out var hand, false);
            card.Initialize(otherPlayer);

            // First player places 2 allies on the board
            {
                firstPlayer.IncreasePower(100);

                firstPlayer.cardManagement.VillainDeck.PutAllOnTop(c => c is AllyCard);

                card.GameEvents.Any(e => e.GameEvent is GameEventShouldContainCards).Should().BeTrue();
                card.GameEvents.ForEach(e => e.GameEvent.Initialize(otherPlayer));
                firstPlayer.cardManagement.FateDeck.AddCard(card);

                firstPlayer.cardManagement.FillHand();

                firstPlayer.Realm.Move(firstPlayer.Realm.Locations[2]).Should().BeTrue();
                firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventGainPower)
                    .Execute();
                firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventPlayCard)
                    .Execute();
                firstPlayer.CurrentState.Should().Be(Villain.State.PlayCard);

                firstPlayer.PlayVillainCard(firstPlayer.cardManagement.Hand.First(), firstPlayer.Realm.CurrentLocation);
                firstPlayer.PlayVillainCard(firstPlayer.cardManagement.Hand.First(), firstPlayer.Realm.CurrentLocation);

                firstPlayer.EndTurn();
            }
            
            // Other player should fate the guard to the same location
            {
                otherPlayer.StartTurn();
                otherPlayer.cardManagement.FillHand();
                otherPlayer.Realm.Move(otherPlayer.Realm.Locations[3]);
                otherPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventFate)
                    .Execute();
                otherPlayer.SetState(Villain.State.PickFateCard, new PickFateCardEventArgs { Target = firstPlayer });
                var fateTargetLocation = firstPlayer.Realm.CurrentLocation;
                var possibleCards = firstPlayer.GetFateOptions();
                possibleCards.First().Should().Be(card);
                otherPlayer.SetState(Villain.State.PlayFateCard,
                    new PlayFateCardEventArgs { Target = otherPlayer, Card = possibleCards.First() });
                otherPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);
                firstPlayer.Fate(possibleCards, possibleCards.First(), fateTargetLocation, otherPlayer);
                otherPlayer.EndTurn();
            }
        }
    }
}