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
            Assert.AreEqual(10 + 7 * card.rarityLevel, card.stats.Values.Sum());
        }
    }
}
