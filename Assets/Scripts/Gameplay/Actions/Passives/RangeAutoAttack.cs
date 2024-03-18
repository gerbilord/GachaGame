using Unity.VisualScripting;

// This monster can target any monster with autoattacks, dealing X(25)% damage to backline.
public class RangeAutoAttack : IPassive
{
    public void OnGetPossibleAutoAttackTargets(GetPossibleTargetsContext context)
    {
        context.payloadPossibleTargets.AddRange(BoardUtils.GetEnemyBoard(context.monster, context.playerBoard1, context.playerBoard2).GetMonsters());
    }
    
    public void OnAutoAttack(AutoAttackContext context)
    {
        if (BoardUtils.IsMonsterInBackline(context.autoAttackTarget, context.playerBoard1, context.playerBoard2))
        {
            context.payloadDamage = context.payloadDamage / 4 * MonsterUtils.GetSpecialValue(context.monster, this);
        }
    }
}