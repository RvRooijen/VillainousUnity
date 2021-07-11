using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public class CardTests : GameTestBase
    {
        protected Card CreateCard(string path, Game game, out Villain firstPlayer, out List<Card> hand)
        {
            Card card = Object.Instantiate(AssetDatabase.LoadAssetAtPath<Card>(path));
            firstPlayer = game.Players.First();
            hand = firstPlayer.cardManagement.Hand;
            hand.Add(card);
            hand.Should().Contain(card);
            return card;
        }
    }
}