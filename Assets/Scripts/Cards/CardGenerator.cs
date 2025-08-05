using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class CardGenerator
{
    // Rarity constants
    private const int MIN_RARITY = 1;
    private const int MAX_RARITY = 8;
    private const float RARITY_INCREASE_CHANCE = 0.25f;
    
    // Special mechanics constants
    private const float NIGHTMARE_CHANCE = 0.01f;
    private const int MAX_SPECIAL_VALUE = 5;
    private const float SPECIAL1_DECAY_CHANCE = 0.25f;
    private const float SPECIAL2_DECAY_CHANCE = 0.5f;
    
    // Stat weights for random distribution
    private const float ATTACK_WEIGHT = 0.25f;
    private const float HP_WEIGHT = 0.25f;
    private const float SPECIAL1_WEIGHT = 0.125f;
    private const float SPECIAL2_WEIGHT = 0.125f;
    private const float MANA_WEIGHT = 0.083f;
    private const float ARMOR_WEIGHT = 0.083f;
    private const float RESIST_WEIGHT = 0.084f;

    public static CardData GenerateCard()
    {
        
        int currentRarity = MIN_RARITY;

        while (RollPercent(RARITY_INCREASE_CHANCE) && currentRarity < MAX_RARITY)
        {
            currentRarity++;
        }
        
        return GenerateCardDataForRarity(currentRarity);
    }

    private static CardData GenerateCardDataForRarity(int rarityLevel)
    {
        if (rarityLevel < MIN_RARITY || rarityLevel > MAX_RARITY)
        {
            throw new ArgumentOutOfRangeException(nameof(rarityLevel), $"Rarity level must be between {MIN_RARITY} and {MAX_RARITY}.");
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
        if (RollPercent(NIGHTMARE_CHANCE))
        {
            // Transfer Armor and Resist values to Special3 and Special4
            card.stats[Stat.Special3] = card.stats[Stat.Armor];
            card.stats[Stat.Special4] = card.stats[Stat.Resist];
        
            // Remove Armor and Resist
            card.stats[Stat.Armor] = 0;
            card.stats[Stat.Resist] = 0;
        
            // Mark as Nightmare (we'll need to add this field to CardData)
            card.isNightmare = true;
            
            Debug.Log($"Card {card.creatureType} has been transformed into a Nightmare!");
        }
    }

    private static void GiveExtraStatsBasedForRarity(int rarityLevel, CardData newCard)
    {
        Rarity rarity = Rarity.GetRarity(rarityLevel);
        int totalExtraStatPoints = rarity.extraStats;

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
            (Stat.Attack, ATTACK_WEIGHT),
            (Stat.Hp, HP_WEIGHT),
            (Stat.Special1, SPECIAL1_WEIGHT),
            (Stat.Special2, SPECIAL2_WEIGHT),
            (Stat.Mana, MANA_WEIGHT),
            (Stat.Armor, ARMOR_WEIGHT),
            (Stat.Resist, RESIST_WEIGHT)
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
                    if (StatUtils.IsSpecialStat(stat) && card.stats[stat] >= MAX_SPECIAL_VALUE)
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
        if (RollPercent(SPECIAL1_DECAY_CHANCE) && card.stats[Stat.Special1] > 0)
        {
            int special1Points = card.stats[Stat.Special1];
            card.stats[Stat.Special1] = 0;
            AssignRandomNonSpecialStatPoint(card, special1Points);
        }
        
        if (RollPercent(SPECIAL2_DECAY_CHANCE) && card.stats[Stat.Special2] > 0)
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