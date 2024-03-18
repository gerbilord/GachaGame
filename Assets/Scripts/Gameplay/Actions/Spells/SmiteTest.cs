using System.Collections.Generic;

public class SmiteTest : ISpell
{
    public DamageType GetDamageType()
    {
        return DamageType.Magical; 
    }

    public string GetName()
    {
        return "SmiteTest";
    }

    public List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        List<Monster> possibleTargets = new List<Monster>();
        foreach (Monster enemy in BoardUtils.GetEnemyBoard(monster, playerBoard1, playerBoard2).GetMonsters())
        {
            possibleTargets.Add(enemy);
        }

        return possibleTargets;
    }

    public PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        Monster caster = BoardUtils.GetMonster(playerAction.monsterId, playerBoard1, playerBoard2);
        Monster target = BoardUtils.GetMonster(playerAction.targetIds[0], playerBoard1, playerBoard2);
        target.TakeDamage(10, this);

        return playerAction;
    }
}