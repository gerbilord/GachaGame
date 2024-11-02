using System.Collections.Generic;

public class Card
{
    public string name { get; set; }
    public Dictionary<Stat, int> stats = new Dictionary<Stat, int>();
    public bool isMagic { get; set; }

    public Card(string name, int attack, int hp, int armor, int resist, int special1, int special2, int mana, bool isMagic)
    {
        this.name = name;
        stats.Add(Stat.Attack, attack);
        stats.Add(Stat.Hp, hp);
        stats.Add(Stat.Armor, armor);
        stats.Add(Stat.Resist, resist);
        stats.Add(Stat.Special1, special1);
        stats.Add(Stat.Special2, special2);
        stats.Add(Stat.Mana, mana);
        this.isMagic = isMagic;
    }
    
    public Card(string name, Dictionary<Stat, int> stats, bool isMagic)
    {
        this.name = name;
        this.stats = new Dictionary<Stat, int>(stats);
        this.isMagic = isMagic;
    }

    public Card DeepCopy()
    {
        return new Card(
            name,
            new Dictionary<Stat, int>(stats),
            isMagic
        );
    }

    public override string ToString()
    {
        string statOutput = "";
        foreach (var stat in stats)
        {
            statOutput += $"{stat.Key}: {stat.Value}\n";
        }

        return $"{name}\n" + 
               $"{statOutput}Magic: {(isMagic ? "Yes" : "No")}";
    }

}