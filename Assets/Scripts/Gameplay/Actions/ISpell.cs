using System.Collections.Generic;

public interface ISpell
{
    string GetName();
    List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
}

public class AutoAttack : ISpell
{
    public string GetName()
    {
        return "AutoAttack";
    }

    public List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        return new();
    }

    public PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        Monster monster = BoardUtils.GetMonster(playerAction.monsterId, playerBoard1, playerBoard2);

        PlayerBoard enemyBoard = BoardUtils.GetEnemyBoard(monster, playerBoard1, playerBoard2);
        PlayerBoard friendlyBoard = BoardUtils.GetMyBoard(monster, playerBoard1, playerBoard2);
        
        int myIndex = friendlyBoard.GetMonsters().IndexOf(monster);

        Monster autoAttackTarget = enemyBoard.GetMonsters()[myIndex];
        autoAttackTarget.TakeDamage(monster.GetAttack(), null);

        PlayerAction truePlayerAction = playerAction.DeepCopy();
        truePlayerAction.targetIds = new List<int> {autoAttackTarget.GetId()};
        return truePlayerAction;
    }
}

public class SmiteTest : ISpell
{
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
        target.TakeDamage(10, null);

        return playerAction;
    }
}

