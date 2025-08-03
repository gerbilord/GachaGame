using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardGenerator
{
    public CardData generateCardDataForRarity(int rarityLevel)
    {
        BasicCardData baseCard = BasicCardData.baseCardsToGenerateFrom[Random.Range(0, BasicCardData.baseCardsToGenerateFrom.Count)];
        
        CardData newCard = baseCard.Copy();
        
        newCard.isMagic = RollPercent(baseCard.chanceIsMagic);
        
        int totalExtraStatPoints = rarityLevel == 0 ? 7 : 7 * rarityLevel;
        
        for (int i = 0; i < totalExtraStatPoints; i++)
        {
            AssignRandomStatPoint(newCard);
        }
        
        // Nightmare transformation - very rare chance
        if (RollPercent(0.01f)) // 1% chance to become a Nightmare
        {
            TransformIntoNightmare(newCard);
        }
        
        ApplySpecialDecay(newCard);
        
        return newCard;
    }
    
    private void AssignRandomStatPoint(CardData card)
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
        
        float roll = Random.Range(0f, 1f);
        float cumulativeChance = 0f;
        
        foreach (var (stat, weight) in statWeights)
        {
            cumulativeChance += weight;
            if (roll < cumulativeChance)
            {
                card.stats[stat]++;
                return;
            }
        }
        
        throw new Exception("Failed to assign a stat point. This should never happen.");
    }
    
    private bool RollPercent(float percentage)
    {
        return Random.Range(0f, 1f) < percentage;
    }
    
    private void ApplySpecialDecay(CardData card)
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
        
        card.stats[Stat.Special1] = Math.Min(card.stats[Stat.Special1], 5);
        card.stats[Stat.Special2] = Math.Min(card.stats[Stat.Special2], 5);
    }
    
    private void AssignRandomNonSpecialStatPoint(CardData card, int points)
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
    
    private void TransformIntoNightmare(CardData card)
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