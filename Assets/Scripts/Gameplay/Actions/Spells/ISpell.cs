using System.Collections.Generic;

public interface ISpell
{
    string GetName();
    List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    DamageType GetDamageType();
}