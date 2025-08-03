using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/**
 * Card
 */
public class BasicCardData : CardData
{
    public static BasicCardData Goblin = new BasicCardData(CreatureType.Goblin,  2, 2, 0, 0, 2, 1, 0, 0, 0, 0.2f);
    public static BasicCardData Fairy = new BasicCardData(CreatureType.Fairy, 1, 1, 0, 2, 1, 1, 0, 0,1, 1);
    public static BasicCardData Witch = new BasicCardData(CreatureType.Witch, 2, 1, 0, 1, 1, 1, 0, 0,1, 1);
    public static BasicCardData Demon = new BasicCardData(CreatureType.Demon, 1, 2, 1, 1, 1, 1, 0, 0,0, 0.8f);
    public static BasicCardData Werewolf = new BasicCardData(CreatureType.Werewolf, 2, 1, 2, 0, 1, 1, 0, 0,0, 0.4f);
    public static BasicCardData Giant = new BasicCardData(CreatureType.Giant, 2, 2, 2, 0, 1, 0, 0, 0,0, 0.2f);
    public static BasicCardData Vampire = new BasicCardData(CreatureType.Vampire, 1, 1, 1, 1, 1, 1, 0, 0,1, 0.6f);
    public static BasicCardData Abomination = new BasicCardData(CreatureType.Abomination, 1, 2, 0, 1, 1, 1, 0, 0,1, 0.8f);
    public static BasicCardData Ghost = new BasicCardData(CreatureType.Ghost, 0, 1, 0, 0, 2, 2, 0, 0,2, 0.6f);
    public static BasicCardData Ghoul = new BasicCardData(CreatureType.Ghoul, 2, 1, 1, 1, 1, 0, 0, 0,1, 0.4f);
    public static BasicCardData Zombie = new BasicCardData(CreatureType.Zombie, 1, 1, 0, 1, 2, 2, 0, 0,0, 0);
    public static BasicCardData Vermin = new BasicCardData(CreatureType.Vermin, 2, 1, 1, 0, 2, 1, 0, 0,0, 0);
    
    public static List<BasicCardData> baseCardsToGenerateFrom = new List<BasicCardData>
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

    public BasicCardData(CreatureType type, int attack, int hp, int armor, int resist, int special1, int special2, int special3, int special4, int mana, float chanceIsMagic) : base(type, attack, hp, armor, resist, special1, special2, special3, special4, mana)
    {
        this.chanceIsMagic = chanceIsMagic;
    }
}