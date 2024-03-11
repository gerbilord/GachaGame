using System.Collections.Generic;
using UnityEngine;

public static class TestUtils
{
    private static int _id = 0;
    public static Hunter CreateHunter()
    {
        return new Hunter(null); // TODO give the hunter a passive
    }

    private static Monster CreateDemon()
    {
        return new Monster(_id++, "Demon", 1, 2, 1, 1, 1, 1, 0, .8f, null, null);
    }

    public static List<Monster> CreateMonsters()
    {
        List<Monster> monsters = new List<Monster>();
        monsters.Add(CreateDemon());
        monsters.Add(CreateDemon());
        monsters.Add(CreateDemon());
        monsters.Add(CreateDemon());
        monsters.Add(CreateDemon());
        return monsters;
    }
    
    public static int GetRandomMonsterId(PlayerBoard playerBoard)
    {
        List<Monster> monsters = playerBoard.GetMonsters();
        return monsters[Random.Range(0, monsters.Count)].GetId();
    }
}