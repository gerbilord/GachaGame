using System.Collections.Generic;

public class CardData
{
    public CreatureType creatureType { get; set; }
    public Dictionary<Stat, int> stats = new Dictionary<Stat, int>();
    public bool isMagic { get; set; }
    public bool isNightmare { get; set; }

    public CardData(CreatureType type, int attack, int hp, int armor, int resist, int special1, int special2, int special3, int special4, int mana)
    {
        creatureType = type;
        stats.Add(Stat.Attack, attack);
        stats.Add(Stat.Hp, hp);
        stats.Add(Stat.Armor, armor);
        stats.Add(Stat.Resist, resist);
        stats.Add(Stat.Special1, special1);
        stats.Add(Stat.Special2, special2);
        stats.Add(Stat.Special3, special3);
        stats.Add(Stat.Special4, special4);
        stats.Add(Stat.Mana, mana);
        this.isMagic = false;
        this.isNightmare = false;
    }
    
    private CardData(CreatureType type, Dictionary<Stat, int> stats, bool isMagic, bool isNightmare)
    {
        this.creatureType = type;
        this.stats = new(stats);
        this.isMagic = isMagic;
        this.isNightmare = isNightmare;
    }

    public CardData Copy()
    {
        var copy = new CardData(
            creatureType,
            new Dictionary<Stat, int>(stats),
            isMagic,
            isNightmare
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

        return $"Type: {creatureType}\n" +
               $"{statOutput}Magic: {(isMagic ? "Yes" : "No")}\n" +
               $"Nightmare: {(isNightmare ? "Yes" : "No")}";
    }

}