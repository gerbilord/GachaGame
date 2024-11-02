using System;
using System.Collections.Generic;

public class BaseCard : Card
{
    public static BaseCard Goblin = new BaseCard("Goblin", 2, 2, 0, 0, 2, 1, 0, 0.2f);
    public static BaseCard Fairy = new BaseCard("Fairy", 1, 1, 0, 2, 1, 1, 1, 1);
    public static BaseCard Witch = new BaseCard("Witch", 2, 1, 0, 1, 1, 1, 1, 1);
    public static BaseCard Demon = new BaseCard("Demon", 1, 2, 1, 1, 1, 1, 0, 0.8f);
    public static BaseCard Werewolf = new BaseCard("Werewolf", 2, 1, 2, 0, 1, 1, 0, 0.4f);
    public static BaseCard Giant = new BaseCard("Giant", 2, 2, 2, 0, 1, 0, 0, 0.2f);
    public static BaseCard Vampire = new BaseCard("Vampire", 1, 1, 1, 1, 1, 1, 1, 0.6f);
    public static BaseCard Abomination = new BaseCard("Abomination", 1, 2, 0, 1, 1, 1, 1, 0.8f);
    public static BaseCard Ghost = new BaseCard("Ghost", 0, 1, 0, 0, 2, 2, 2, 0.6f);
    public static BaseCard Ghoul = new BaseCard("Ghoul", 2, 1, 1, 1, 1, 0, 1, 0.4f);
    public static BaseCard Zombie = new BaseCard("Zombie", 1, 1, 0, 1, 2, 2, 0, 0);
    public static BaseCard Vermin = new BaseCard("Vermin", 2, 1, 1, 0, 2, 1, 0, 0);
    
    public static List<BaseCard> baseCards = new List<BaseCard>
    {
        Goblin,
        Fairy,
        Witch,
        Demon,
        Werewolf,
        Giant,
        Vampire,
        Abomination,
        Ghost,
        Ghoul,
        Zombie,
        Vermin
    };

    
    public float chanceIsMagic { get; set; }

    public BaseCard(string name, int attack, int hp, int armor, int resist, int special1, int special2, int mana, float chanceIsMagic ) : base(name, attack, hp, armor, resist, special1, special2, mana, false)
    {
        this.chanceIsMagic = chanceIsMagic;
    }
    
    private Card generateCard()
    {
        return new Card(
            name,
            stats,
            getIsMagic()
        );
    }

    private Card generateRarityCard(int rarityLevel)
    {
        Rarity rarity = Rarity.GetRarity(rarityLevel);
        Card card = generateCard();
        card.name = rarity.rarityName + " " + card.name;

        for(int i = 0; i < rarity.rarityStats; i++)
        {
            Stat stat = getWeightedRandomStat();
            card.stats[stat] += 1;
        }

        decaySpecials(card);


        return card;
    }

    private void decaySpecials(Card card)
    {
        List<Stat> decayedStats = new List<Stat>();
        bool special1Decayed = rollPercentChance(.25);
        bool special2Decayed = rollPercentChance(.5);
        
        int totalStatsToDecay = 0;
        if(special1Decayed)
        {
            totalStatsToDecay += stats[Stat.Special1];
            stats[Stat.Special1] = 0;
            decayedStats.Add(Stat.Special1);
        }
        if(special2Decayed)
        {
            totalStatsToDecay += stats[Stat.Special2];
            stats[Stat.Special2] = 0;
            decayedStats.Add(Stat.Special2);
        }
        
        for(int i = 0; i < totalStatsToDecay; i++)
        {
            Stat stat = getWeightedRandomStatWithout(decayedStats);
            card.stats[stat] += 1;
        }
    }

    private bool rollPercentChance(double chance)
    {
        Random random = new Random();
        return random.NextDouble() < chance;
    }
    
    private Stat getWeightedRandomStatWithout(List<Stat> excludedStats)
    {
        Stat stat = excludedStats[0];

        while (excludedStats.Contains(stat))
        {
            stat = getWeightedRandomStat();
        }

        return stat;
    }
    
    private Stat getWeightedRandomStat()
    {
        // 25% chance to get attack
        // 25% chance to get hp
        // 12.5% chance to get special 1
        // 12.5% chance to get special 2
        // 8.33% chance to get armor
        // 8.33% chance to get resist
        // 8.33% chance to get mana
        Random random = new Random();
        double randomDouble = random.NextDouble();
        if(randomDouble < 0.25)
        {
            return Stat.Attack;
        }
        else if(randomDouble < 0.5)
        {
            return Stat.Hp;
        }
        else if(randomDouble < 0.625)
        {
            return Stat.Special1;
        }
        else if(randomDouble < 0.75)
        {
            return Stat.Special2;
        }
        else if(randomDouble < 0.833)
        {
            return Stat.Armor;
        }
        else if(randomDouble < 0.9166)
        {
            return Stat.Resist;
        }
        else
        {
            return Stat.Mana;
        }
        
    }
    
    private Stat getRandomStat()
    {
        Random random = new Random();
        int statIndex = random.Next(0, 7);
        return (Stat)statIndex;
    }
    
    private bool getIsMagic()
    {
        Random random = new Random();
        return random.NextDouble() < chanceIsMagic;
        
    }

    public static Card GenerateRandomCardWithRarity(int rarityLevel)
    {
        BaseCard baseCard = GetRandomBaseCard();
        return baseCard.generateRarityCard(rarityLevel);
    }
    
    public static BaseCard GetRandomBaseCard()
    {
        Random random = new Random();
        int randomIndex = random.Next(0, baseCards.Count);
        return baseCards[randomIndex];
    }
    
    // To string override
    public override string ToString()
    {
        return base.ToString();
    }
}