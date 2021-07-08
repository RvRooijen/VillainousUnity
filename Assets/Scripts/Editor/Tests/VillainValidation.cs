using System;
using System.Linq;
using Editor.ObjectProviders;
using FluentAssertions;
using NUnit.Framework;

namespace Editor.Tests
{
    [Category("Asset Validation")]
    [TestFixture]
    [TestFixtureSource(typeof(VillainDataProvider))]
    public class VillainValidation
    {
        private Villain _villain;
        private VillainData _villainData;
    
        public VillainValidation(VillainData template)
        {
            _villainData = template;
            Game game = new Game(new GameSettings(){Random = new Random(0)}, template);
            _villain = game.Players.First();
        }
    
        [Test, Order(1)]
        public void VillainShouldBeCreated()
        {
            _villain.Should().NotBeNull();
        }
    
        [Test, Order(2)]
        public void VillainShouldBeCorrectType()
        {
            _villain.GetType().Should().Be(_villainData.BehaviorType);
        }
    
        
        [Test, Order(3)]
        public void VillainShouldHaveValidDeck()
        {
            Assert.NotNull(_villain.Deck);
            _villain.Deck.FateDeck.Count.Should().Be(15);
            _villain.Deck.VillainDeck.Count.Should().Be(30);
            _villain.Deck.FateDiscard.Count.Should().Be(0);
            _villain.Deck.VillainDiscard.Count.Should().Be(0);
        }
    
        [Test, Order(4)]
        public void VillainShouldDrawCards()
        {
            _villain.Deck.FillHand();
            _villain.Deck.Hand.Count.Should().Be(4);
        }
    
        [Test]
        public void VillainShouldHaveRealm()
        {
            _villain.Realm.Should().NotBeNull();
        }
    
        [Test]
        public void VillainShouldHaveFourLocationsInRealm()
        {
            _villain.Realm.Locations.Count.Should().Be(4);
        }
    
        [Test]
        public void VillainShouldHaveActionsAtLocations()
        {
            foreach (Location location in _villain.Realm.Locations)
            {
                location.PlayerActions.Count.Should().BeOneOf(2,4);
            }
        }
    }
}
