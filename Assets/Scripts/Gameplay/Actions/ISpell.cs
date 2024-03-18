using System.Collections.Generic;
using System.Linq;

public interface ISpell
{
    string GetName();
    List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    DamageType GetDamageType();
}

public class AutoAttack : ISpell
{
    private DamageType _damageType;
    
    public AutoAttack(DamageType damageType)
    {
        _damageType = damageType;
    }

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
        Monster autoAttackTarget = BoardUtils.GetEnemyMonsterAcross(monster, playerBoard1, playerBoard2); 

        autoAttackTarget.TakeDamage(monster.GetAttack(), this);

        PlayerAction truePlayerAction = playerAction.DeepCopy();
        truePlayerAction.targetIds = new List<int> { autoAttackTarget.GetId() };
        return truePlayerAction;
    }

    public DamageType GetDamageType()
    {
        return _damageType;
    }
}

public class Swap : ISpell
{
    public DamageType GetDamageType()
    {
        return DamageType.Physical; 
    }

    public string GetName()
    {
        return "Swap";
    }

    public List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        return BoardUtils.GetMyBoard(monster, playerBoard1, playerBoard2).GetMonsters().Where(aMonster=> aMonster != monster).ToList();
    }

    public PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        Monster monster = BoardUtils.GetMonster(playerAction.monsterId, playerBoard1, playerBoard2);
        Monster target = BoardUtils.GetMonster(playerAction.targetIds[0], playerBoard1, playerBoard2);
        
        PlayerBoard myBoard = BoardUtils.GetMyBoard(monster, playerBoard1, playerBoard2);
        
        myBoard.SwapMonsters(monster, target);
        return playerAction;
    }
}

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

