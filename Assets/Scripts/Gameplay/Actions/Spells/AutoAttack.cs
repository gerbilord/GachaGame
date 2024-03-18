using System.Collections.Generic;
using System.Linq;

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
        GetPossibleTargetsContext getPossibleTargetsContext = new GetPossibleTargetsContext
        {
            spell = this,
            monster = monster,
            playerBoard1 = playerBoard1,
            playerBoard2 = playerBoard2,
            payloadPossibleTargets = new HashSet<Monster>()
        };

        monster.GetPassives().ForEach(passive=> passive.OnGetPossibleAutoAttackTargets(getPossibleTargetsContext));

        return getPossibleTargetsContext.payloadPossibleTargets.ToList();
    }

    public PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        Monster monster = BoardUtils.GetMonster(playerAction.monsterId, playerBoard1, playerBoard2);
        Monster autoAttackTarget = BoardUtils.GetEnemyMonsterAcross(monster, playerBoard1, playerBoard2);

        if(playerAction.targetIds is { Count: > 0 })
        {
            autoAttackTarget = BoardUtils.GetMonster(playerAction.targetIds[0], playerBoard1, playerBoard2);
        }

        AutoAttackContext autoAttackContext = new AutoAttackContext
        {
            monster = monster,
            autoAttackTarget = autoAttackTarget,
            playerBoard1 = playerBoard1,
            playerBoard2 = playerBoard2,
            payloadDamage = monster.GetAttack()
        };

        monster.GetPassives().ForEach(passive=> passive.OnAutoAttack(autoAttackContext));

        autoAttackTarget.TakeDamage(autoAttackContext.payloadDamage, this);

        PlayerAction truePlayerAction = playerAction.DeepCopy();
        truePlayerAction.targetIds = new List<int> { autoAttackTarget.GetId() };
        return truePlayerAction;
    }

    public DamageType GetDamageType()
    {
        return _damageType;
    }
}