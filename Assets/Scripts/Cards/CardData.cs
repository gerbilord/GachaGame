using System.Collections.Generic;

public class CardData
{
    public CreatureType creatureType { get; set; }
    public Dictionary<Stat, int> stats = new Dictionary<Stat, int>();
    public bool isMagic { get; set; }
    public bool isNightmare { get; set; }
    public int rarityLevel { get; set; }

    public Special special1 { get; set; }
    public Special special2 { get; set; }
    public Special special3 { get; set; }
    public Special special4 { get; set; }

    public CardData(CreatureType type, int attack, int hp, int armor, int resist, int special1Stat, int special2Stat, int special3Stat, int special4Stat, int mana)
    {
        creatureType = type;
        stats.Add(Stat.Attack, attack);
        stats.Add(Stat.Hp, hp);
        stats.Add(Stat.Armor, armor);
        stats.Add(Stat.Resist, resist);
        stats.Add(Stat.Special1, special1Stat);
        stats.Add(Stat.Special2, special2Stat);
        stats.Add(Stat.Special3, special3Stat);
        stats.Add(Stat.Special4, special4Stat);
        stats.Add(Stat.Mana, mana);
        this.special1 = null;
        this.special2 = null;
        this.special3 = null;
        this.special4 = null;
        this.isMagic = false;
        this.isNightmare = false;
    }
    
    private CardData(CreatureType type, Dictionary<Stat, int> stats, bool isMagic, bool isNightmare, int rarityLevel)
    {
        this.creatureType = type;
        this.stats = new(stats);
        this.isMagic = isMagic;
        this.isNightmare = isNightmare;
        this.rarityLevel = rarityLevel;
    }

    public CardData Copy()
    {
        var copy = new CardData(
            creatureType,
            new Dictionary<Stat, int>(stats),
            isMagic,
            isNightmare,
            rarityLevel
        );
        return copy;
    }

    public override string ToString()
    {
        string statOutput = "";
        foreach (var stat in stats)
        {
            statOutput += $"{stat.Key}: {stat.Value}\n";
        }

        return $"{(isNightmare ? "Nightmarish " : "")}"+ $"{Rarity.GetRarity(rarityLevel).rarityName} " + $"{creatureType}\n" +
               $"{statOutput}Magic: {(isMagic ? "Yes" : "No")}\n";
    }
}