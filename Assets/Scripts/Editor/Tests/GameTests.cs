using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Editor.Tests
{
    public class GameTests : GameTestBase
    {
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
            firstPlayer.cardManagement.FillHand();
            firstPlayer.cardManagement.Hand.Count.Should().Be(4);
            firstPlayer.CurrentState.Should().Be(Villain.State.Move);
        }
    }
}