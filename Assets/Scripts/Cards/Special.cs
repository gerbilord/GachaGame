using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Special
{
public static Special putridSpell1 = new Special(SpecialCategory.Putrid, SpecialUsage.Spell, "putrid spell 1", "Generate a Poison(X) cloud at target position that floats forward each turn for the rest of the game.", 4);
public static Special putridSpell2 = new Special(SpecialCategory.Putrid, SpecialUsage.Spell, "putrid spell 2", "Poison(X) target monster for 2 turns.", 3);
public static Special putridSpell3 = new Special(SpecialCategory.Putrid, SpecialUsage.Spell, "putrid spell 3", "ALL monsters have -2(X) resist for 2 turns.", 5);

public static Special swarmSpell1 = new Special(SpecialCategory.Swarm, SpecialUsage.Spell, "swarm spell 1", "This monster and all its counterparts attack together for 1 turn.", 6);
public static Special swarmSpell2 = new Special(SpecialCategory.Swarm, SpecialUsage.Spell, "swarm spell 2", "Overwhelm target monster for (X) turns.", 4);
public static Special swarmSpell3 = new Special(SpecialCategory.Swarm, SpecialUsage.Spell, "swarm spell 3", "Your monsters have +(X) autoattack damage for 2 turns.", 4);

public static Special shifterSpell1 = new Special(SpecialCategory.Shifter, SpecialUsage.Spell, "shifter spell 1", "This monster morphs to look like its counterpart and permanently copies its passives as if they were level (X).", 5);
public static Special shifterSpell2 = new Special(SpecialCategory.Shifter, SpecialUsage.Spell, "shifter spell 2", "Obscure target friendly monster for (X) turns.", 3);
public static Special shifterSpell3 = new Special(SpecialCategory.Shifter, SpecialUsage.Spell, "shifter spell 3", "This monster's counterpart becomes Obscured for (X) turns.", 3);

public static Special dreadSpell1 = new Special(SpecialCategory.Dread, SpecialUsage.Spell, "dread spell 1", "This monster has +2 autoattack damage and +(X) resist for 2 turns.", 5);
public static Special dreadSpell2 = new Special(SpecialCategory.Dread, SpecialUsage.Spell, "dread spell 2", "Stun target monster for (X) turns.", 3);
public static Special dreadSpell3 = new Special(SpecialCategory.Dread, SpecialUsage.Spell, "dread spell 3", "Stun the 3 monsters adjacent to the target for (X) turns.", 5);

public static Special revenantSpell1 = new Special(SpecialCategory.Revenant, SpecialUsage.Spell, "revenant spell 1", "Resurrect a friendly monster with (X)(20)% max HP.", 6);
public static Special revenantSpell2 = new Special(SpecialCategory.Revenant, SpecialUsage.Spell, "revenant spell 2", "Cleanse target monster.", 3);
public static Special revenantSpell3 = new Special(SpecialCategory.Revenant, SpecialUsage.Spell, "revenant spell 3", "Restore 10 HP to all friendly monsters each for (X) turns.", 4);

public static Special wardenSpell1 = new Special(SpecialCategory.Warden, SpecialUsage.Spell, "warden spell 1", "This monster grows, increasing its damage and health by (X).", 4);
public static Special wardenSpell2 = new Special(SpecialCategory.Warden, SpecialUsage.Spell, "warden spell 2", "Empower both this monster and its counterpart with +(X) autoattack damage for 2 turns.", 3);
public static Special wardenSpell3 = new Special(SpecialCategory.Warden, SpecialUsage.Spell, "warden spell 3", "Autoattack the monster behind the target for (X)(20)% damage.", 3);

public static Special arcaneSpell1 = new Special(SpecialCategory.Arcane, SpecialUsage.Spell, "arcane spell 1", "Mark this monster's next autoattack to deal (X)(20)% damage to the backline.", 5);
public static Special arcaneSpell2 = new Special(SpecialCategory.Arcane, SpecialUsage.Spell, "arcane spell 2", "Deal 10(X) damage to front enemy monster.", 6);
public static Special arcaneSpell3 = new Special(SpecialCategory.Arcane, SpecialUsage.Spell, "arcane spell 3", "Shackle (X) target monster for 2 turns.", 4);

public static Special cursedSpell1 = new Special(SpecialCategory.Cursed, SpecialUsage.Spell, "cursed spell 1", "Your midline autoattacks apply Curse for (X) turns.", 5);
public static Special cursedSpell2 = new Special(SpecialCategory.Cursed, SpecialUsage.Spell, "cursed spell 2", "Curse target position. Monsters standing there take 5(X) damage from your next attack.", 3);
public static Special cursedSpell3 = new Special(SpecialCategory.Cursed, SpecialUsage.Spell, "cursed spell 3", "Silence target monster for (X) turns.", 2);

public static Special putridPassive1 = new Special(SpecialCategory.Putrid, SpecialUsage.Passive, "putrid passive 1", "This monster's counterpart cannot be healed and has -(X) resist.", 0);
public static Special putridPassive2 = new Special(SpecialCategory.Putrid, SpecialUsage.Passive, "putrid passive 2", "Autoattacks Poison(X) target for 2 turns.", 0);
public static Special putridPassive3 = new Special(SpecialCategory.Putrid, SpecialUsage.Passive, "putrid passive 3", "Each time this monster swaps positions, Poison(X) its new counterpart for 2 turns.", 0);

public static Special swarmPassive1 = new Special(SpecialCategory.Swarm, SpecialUsage.Passive, "swarm passive 1", "This monster’s HP is split into (X) additional monsters that all attack together. Only one can be targeted at once.", 0);
public static Special swarmPassive2 = new Special(SpecialCategory.Swarm, SpecialUsage.Passive, "swarm passive 2", "Autoattacks Overwhelm target for (X) turns and consume (X) Mana.", 0);
public static Special swarmPassive3 = new Special(SpecialCategory.Swarm, SpecialUsage.Passive, "swarm passive 3", "Each time this monster swaps positions, Overwhelm its new counterpart for (X) turns.", 0);

public static Special shifterPassive1 = new Special(SpecialCategory.Shifter, SpecialUsage.Passive, "shifter passive 1", "This monster looks like its first counterpart and permanently copies its passives as if they were level (X).", 0);
public static Special shifterPassive2 = new Special(SpecialCategory.Shifter, SpecialUsage.Passive, "shifter passive 2", "Autoattacks drain 2(X) mana from your opponent.", 0);
public static Special shifterPassive3 = new Special(SpecialCategory.Shifter, SpecialUsage.Passive, "shifter passive 3", "Each time this monster swaps positions, Obscure both it and the monster it swapped with for (X) turns.", 0);

public static Special dreadPassive1 = new Special(SpecialCategory.Dread, SpecialUsage.Passive, "dread passive 1", "This monster’s starting position is highlighted. Monsters standing there have +(X) armor and +(X) resist.", 0);
public static Special dreadPassive2 = new Special(SpecialCategory.Dread, SpecialUsage.Passive, "dread passive 2", "Autoattacks Stun target for (X) turns and consume (X) Mana.", 0);
public static Special dreadPassive3 = new Special(SpecialCategory.Dread, SpecialUsage.Passive, "dread passive 3", "Each time this monster swaps positions, Stun its new counterpart for (X) turns.", 0);

public static Special revenantPassive1 = new Special(SpecialCategory.Revenant, SpecialUsage.Passive, "revenant passive 1", "This monster revives (X) turns after its first death with (X)(20)% max HP.", 0);
public static Special revenantPassive2 = new Special(SpecialCategory.Revenant, SpecialUsage.Passive, "revenant passive 2", "Autoattacks heal this monster for (X)(20)% of damage dealt.", 0);
public static Special revenantPassive3 = new Special(SpecialCategory.Revenant, SpecialUsage.Passive, "revenant passive 3", "Each time this monster swaps positions, heal the monster it is swapped with 5(X) HP and Cleanse it.", 0);

public static Special wardenPassive1 = new Special(SpecialCategory.Warden, SpecialUsage.Passive, "warden passive 1", "If this monster has (X)(20)% HP or less, it is Empowered.", 0);
public static Special wardenPassive2 = new Special(SpecialCategory.Warden, SpecialUsage.Passive, "warden passive 2", "Autoattacks cleave, dealing (X)(20)% damage to the monster behind the target.", 0);
public static Special wardenPassive3 = new Special(SpecialCategory.Warden, SpecialUsage.Passive, "warden passive 3", "Each time this monster swaps positions, Empower both it and the monster it swapped with for (X) turns.", 0);

public static Special arcanePassive1 = new Special(SpecialCategory.Arcane, SpecialUsage.Passive, "arcane passive 1", "This monster can target any monster with autoattacks, dealing (X)(20)% damage to backline.", 0);
public static Special arcanePassive2 = new Special(SpecialCategory.Arcane, SpecialUsage.Passive, "arcane passive 2", "Autoattacks Shackle(X) target for 2 turns.", 0);
public static Special arcanePassive3 = new Special(SpecialCategory.Arcane, SpecialUsage.Passive, "arcane passive 3", "Each time this monster swaps positions, Shackle(X) its new counterpart for 2 turns.", 0);

public static Special cursedPassive1 = new Special(SpecialCategory.Cursed, SpecialUsage.Passive, "cursed passive 1", "Whichever monster kills this monster takes 5(X) damage and is permanently Silenced.", 0);
public static Special cursedPassive2 = new Special(SpecialCategory.Cursed, SpecialUsage.Passive, "cursed passive 2", "Autoattacks Silence target for (X) turns and consume (X) Mana.", 0);
public static Special cursedPassive3 = new Special(SpecialCategory.Cursed, SpecialUsage.Passive, "cursed passive 3", "Each time this monster swaps positions, Silence its new counterpart for (X) turns.", 0);

    public static List<Special> AllSpecials = new List<Special>
    {
        putridSpell1, putridSpell2, putridSpell3,
        swarmSpell1, swarmSpell2, swarmSpell3,
        shifterSpell1, shifterSpell2, shifterSpell3,
        dreadSpell1, dreadSpell2, dreadSpell3,
        revenantSpell1, revenantSpell2, revenantSpell3,
        wardenSpell1, wardenSpell2, wardenSpell3,
        arcaneSpell1, arcaneSpell2, arcaneSpell3,
        cursedSpell1, cursedSpell2, cursedSpell3,
        putridPassive1, putridPassive2, putridPassive3,
        swarmPassive1, swarmPassive2, swarmPassive3,
        shifterPassive1, shifterPassive2, shifterPassive3,
        dreadPassive1, dreadPassive2, dreadPassive3,
        revenantPassive1, revenantPassive2, revenantPassive3,
        wardenPassive1, wardenPassive2, wardenPassive3,
        arcanePassive1, arcanePassive2, arcanePassive3,
        cursedPassive1, cursedPassive2, cursedPassive3
    };

    public string name { get; private set; }
    public string description { get; private set; }
    public SpecialCategory category { get; private set; }
    public SpecialUsage usage { get; private set; }
    public int manaMultiplier { get; private set; } = 0; // Only used if usage is Spell.
    
    private Special(SpecialCategory category, SpecialUsage usage, string specialName, string description, int manaMultiplier = 0)
    {
        this.category = category;
        this.usage = usage;
        this.name = specialName;
        this.description = description;
        this.manaMultiplier = manaMultiplier;
    }
}