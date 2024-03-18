using System.Collections.Generic;

public interface IPassive
{
    public void OnGetPossibleAutoAttackTargets(GetPossibleTargetsContext context) {}
    
    public void OnAutoAttack(AutoAttackContext context) {}
}

public class AutoAttackContext
{
    public Monster monster;
    public Monster autoAttackTarget;
    public PlayerBoard playerBoard1;
    public PlayerBoard playerBoard2;
    public int payloadDamage;
}

public class GetPossibleTargetsContext {
    public ISpell spell;
    public Monster monster;
    public PlayerBoard playerBoard1;
    public PlayerBoard playerBoard2;
    public HashSet<Monster> payloadPossibleTargets = new HashSet<Monster>();
}

public interface IHunterPassive
{
        
}