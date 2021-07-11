using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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
            
            firstPlayer.Realm.Move(firstPlayer.Realm.Locations[3]).Should().BeTrue();
            firstPlayer.Realm.CurrentLocation.PlayerActions.First(action => action.GameEvent is GameEventFate).Execute();

            firstPlayer.CurrentState.Should().Be(Villain.State.Fate);

            var possibleCards = game.Players
                .Last()
                .GetFateOptions();
            
            // Assert
            card.GameEvents.Any(e => e.GameEvent is GameEventRevealAndPlay).Should().BeTrue();
            card.PowerCost.Should().Be(0);
            card.Should().BeOfType<HeroCard>();
        }
    }
}