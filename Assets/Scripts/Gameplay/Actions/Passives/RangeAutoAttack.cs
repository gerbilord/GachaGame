using System;
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
            context.payloadDamage = (int)Math.Ceiling(context.payloadDamage * (.25f * MonsterUtils.GetSpecialValue(context.monster, this)));
        }
    }
}