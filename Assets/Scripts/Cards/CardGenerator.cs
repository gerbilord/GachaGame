using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class CardGenerator
{
    private static readonly int MinRarity = 1;
    private static readonly int MaxRarity = 8;
    private static readonly float NightmareChance = .01f;
    private static readonly int MaxSpecialValue = 5;

    public static CardData GenerateCard()
    {
        
        int currentRarity = MinRarity;

        while (RollPercent(.2f) && currentRarity < MaxRarity)
        {
            currentRarity++;
        }
        
        return GenerateCardDataForRarity(currentRarity);
    }

    private static CardData GenerateCardDataForRarity(int rarityLevel)
    {
        if (rarityLevel < 1 || rarityLevel > MaxRarity)
        {
            throw new ArgumentOutOfRangeException(nameof(rarityLevel), "Rarity level must be between 1 and 8.");
        }

        BasicCardData baseCard = BasicCardData.baseCardsToGenerateFrom[Random.Range(0, BasicCardData.baseCardsToGenerateFrom.Count)];
        
        CardData newCard = baseCard.Copy();
        
        newCard.rarityLevel = rarityLevel;
        newCard.isMagic = RollPercent(baseCard.chanceIsMagic);
        
        GiveExtraStatsBasedForRarity(rarityLevel, newCard);
        ChanceApplySpecialDecay(newCard);
        ChanceMakeNightmare(newCard);

        return newCard;
    }

    private static void ChanceMakeNightmare(CardData card)
    {
        // Nightmare transformation - very rare chance
        if (RollPercent(NightmareChance))
        {
            // Transfer Armor and Resist values to Special3 and Special4
            card.stats[Stat.Special3] = card.stats[Stat.Armor];
            card.stats[Stat.Special4] = card.stats[Stat.Resist];
        
            // Remove Armor and Resist
            card.stats[Stat.Armor] = 0;
            card.stats[Stat.Resist] = 0;
        
            // Mark as Nightmare (we'll need to add this field to CardData)
            card.isNightmare = true;
        }
    }

    private static void GiveExtraStatsBasedForRarity(int rarityLevel, CardData newCard)
    {
        int totalExtraStatPoints = 7 * rarityLevel;

        for (int i = 0; i < totalExtraStatPoints; i++)
        {
            AssignRandomStatPoint(newCard);
        }
    }

    private static void AssignRandomStatPoint(CardData card)
    {
        // Define stat weights - these sum to 1.0
        var statWeights = new List<(Stat stat, float weight)>
        {
            (Stat.Attack, 0.25f),
            (Stat.Hp, 0.25f),
            (Stat.Special1, 0.125f),
            (Stat.Special2, 0.125f),
            (Stat.Mana, 0.083f),
            (Stat.Armor, 0.083f),
            (Stat.Resist, 0.084f)
        };
        
        // Keep rolling until we find a valid stat to increase
        while (true)
        {
            float roll = Random.Range(0f, 1f);
            float cumulativeChance = 0f;
            
            foreach (var (stat, weight) in statWeights)
            {
                cumulativeChance += weight;
                if (roll < cumulativeChance)
                {
                    // Check if this is a special stat and if it would exceed max
                    if (StatUtils.IsSpecialStat(stat) && card.stats[stat] >= MaxSpecialValue)
                    {
                        // Re-roll by breaking the inner loop and continuing the outer loop
                        break;
                    }
                    
                    card.stats[stat]++;
                    return;
                }
            }
        }
    }
    
    
    private static bool RollPercent(float percentage)
    {
        return Random.Range(0f, 1f) < percentage;
    }
    
    private static void ChanceApplySpecialDecay(CardData card)
    {
        if (RollPercent(0.25f) && card.stats[Stat.Special1] > 0)
        {
            int special1Points = card.stats[Stat.Special1];
            card.stats[Stat.Special1] = 0;
            AssignRandomNonSpecialStatPoint(card, special1Points);
        }
        
        if (RollPercent(0.5f) && card.stats[Stat.Special2] > 0)
        {
            int special2Points = card.stats[Stat.Special2];
            card.stats[Stat.Special2] = 0;
            AssignRandomNonSpecialStatPoint(card, special2Points);
        }
    }
    
    private static void AssignRandomNonSpecialStatPoint(CardData card, int points)
    {
        List<Stat> nonSpecialStats = new List<Stat> 
        {
            Stat.Attack, Stat.Hp, Stat.Armor, Stat.Resist, Stat.Mana
        };
        
        for (int i = 0; i < points; i++)
        {
            Stat randomStat = nonSpecialStats[Random.Range(0, nonSpecialStats.Count)];
            card.stats[randomStat]++;
        }
    }
}