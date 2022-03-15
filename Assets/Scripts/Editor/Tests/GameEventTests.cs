using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class GameEventTests : CardTests
    {
        [Test]
        public void GameEventRevealAndPlay()
        {
            Game game = CreateGame();
            var card = ScriptableObject.CreateInstance<HeroCard>();
            card.CardGameEvents.Add(new Card.TriggerEvent(){GameEvent = new GameEventRevealAndPlay(), TriggerType = GameEvent.TriggerType.OnPlay});
            
            var firstPlayer = game.Players.First();
            var hand = firstPlayer.cardManagement.Hand;
            hand.Add(card);
            
            // Assert
            card.CardGameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }
    }
}