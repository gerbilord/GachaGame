using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCardGeneration
{
    [Test]
    public void TestCardGeneration_correctStatAmounts()
    {
        for (int i = 0; i < 100; i++)
        {
            CardData card = CardGenerator.GenerateCard();
            Rarity rarity = Rarity.GetRarity(card.rarityLevel);
            Assert.AreEqual(10 + rarity.extraStats, card.stats.Values.Sum());
        }
    }
}
