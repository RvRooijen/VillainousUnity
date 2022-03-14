using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UnityEditor;
using UnityEngine;

namespace Editor.Tests
{
    public class CardTests : GameTestBase
    {
        protected Card CreateCard(string path, Game game, out Villain firstPlayer, out Villain otherPlayer, out List<Card> hand, bool addToHand)
        {
            Card card = Object.Instantiate(AssetDatabase.LoadAssetAtPath<Card>(path));
            firstPlayer = game.Players.First();
            otherPlayer = game.Players.Last();
            hand = firstPlayer.cardManagement.Hand;
            
            if (addToHand)
            {
                hand.Add(card);
                hand.Should().Contain(card);
            }

            return card;
        }
    }
}