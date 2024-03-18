using System.Collections.Generic;
using UnityEngine;

public static class TestUtils
{
    private static int _id = 0;
    public static Hunter CreateHunter()
    {
        return new Hunter(null); // TODO give the hunter a passive
    }

    private static Monster CreateDemon(string name)
    {
        return new Monster(_id++, name, 1, 2, 1, 1, 1, 1, 0, .8f, DamageType.Physical, new(), new List<ISpell> {new SmiteTest()});
    }

    public static List<Monster> CreateMonsters()
    {
        List<Monster> monsters = new List<Monster>();
        monsters.Add(CreateDemon("Demon King"));
        monsters.Add(CreateDemon("Demon Queen"));
        monsters.Add(CreateDemon("Demon Prince"));
        monsters.Add(CreateDemon("Demon Princess"));
        monsters.Add(CreateDemon("Demon Knight"));
        return monsters;
    }
    
    public static int GetRandomMonsterId(PlayerBoard playerBoard)
    {
        List<Monster> monsters = playerBoard.GetMonsters();
        return monsters[Random.Range(0, monsters.Count)].GetId();
    }
}