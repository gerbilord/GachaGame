using System.Collections.Generic;

public class CreatureType
{
    public static CreatureType Vermin = new CreatureType("Vermin", new List<SpecialCategory> { SpecialCategory.Swarm, SpecialCategory.Putrid });
    public static CreatureType Vampire = new CreatureType("Vampire", new List<SpecialCategory> { SpecialCategory.Dread, SpecialCategory.Revenant });
    public static CreatureType Zombie = new CreatureType("Zombie", new List<SpecialCategory> { SpecialCategory.Putrid, SpecialCategory.Revenant });
    public static CreatureType Fairy = new CreatureType("Fairy", new List<SpecialCategory> { SpecialCategory.Swarm, SpecialCategory.Arcane });
    public static CreatureType Ghoul = new CreatureType("Ghoul", new List<SpecialCategory> { SpecialCategory.Cursed, SpecialCategory.Putrid });
    public static CreatureType Witch = new CreatureType("Witch", new List<SpecialCategory> { SpecialCategory.Arcane, SpecialCategory.Revenant });
    public static CreatureType Ghost = new CreatureType("Ghost", new List<SpecialCategory> { SpecialCategory.Cursed, SpecialCategory.Shifter });
    public static CreatureType Demon = new CreatureType("Demon", new List<SpecialCategory> { SpecialCategory.Warden, SpecialCategory.Arcane });
    public static CreatureType Abomination = new CreatureType("Abomination", new List<SpecialCategory> { SpecialCategory.Cursed, SpecialCategory.Dread });
    public static CreatureType Werewolf = new CreatureType("Werewolf", new List<SpecialCategory> { SpecialCategory.Warden, SpecialCategory.Shifter });
    public static CreatureType Goblin = new CreatureType("Goblin", new List<SpecialCategory> { SpecialCategory.Swarm, SpecialCategory.Shifter });
    public static CreatureType Ogre = new CreatureType("Ogre", new List<SpecialCategory> { SpecialCategory.Dread, SpecialCategory.Warden });


    public static List<CreatureType> AllCreatureTypes = new List<CreatureType>
    {
        Goblin, Fairy, Witch, Demon, Werewolf, Ogre, 
        Vampire, Abomination, Ghost, Ghoul, Zombie, Vermin
    };

    public string Name { get; private set; }
    public List<SpecialCategory> SpecialCategories { get; private set; }

    private CreatureType(string name, List<SpecialCategory> specialCategories)
    {
        Name = name;
        SpecialCategories = specialCategories;
    }

    public override string ToString()
    {
        return Name;
    }
}