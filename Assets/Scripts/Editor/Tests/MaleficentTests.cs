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
        public void LastCardInHand_ShouldBeSinisterGoon()
        {
            Game game = CreateGame();
            var firstPlayer = game.Players.First();
            firstPlayer.cardManagement.FillHand();
            var hand = firstPlayer.cardManagement.Hand;
            hand[3].name.Should().Be("Sinister Goon(Clone)");
        }
        
        [Test]
        public void Card_Aurora()
        {
            Game game = CreateGame();
            var path = "Assets/ScriptableObjects/Maleficent/FateDeck/Aurora.asset";
            var card = CreateCard(path, game, out var firstPlayer, out var hand);
            var otherPlayer = game.Players.Last();
            card.Initialize(otherPlayer);
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[3]).Should().BeTrue();
            
            card.GameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.GameEvents.ForEach(e => e.GameEvent.Initialize(otherPlayer));
            card.GameEvents.ForEach(e => e.GameEvent.Execute(firstPlayer));
            firstPlayer.CurrentState.Should().Be(Villain.State.PlayFateCard);
            
            otherPlayer.Fate(new List<Card> {card}, card, otherPlayer.Realm.Locations.First(), firstPlayer);

            otherPlayer.Realm.Locations.First().PlacedFateCards.Count.Should().Be(1);
            
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }
    }
}