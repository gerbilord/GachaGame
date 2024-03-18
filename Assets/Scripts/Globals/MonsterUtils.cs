public static class MonsterUtils
{
    public static int GetSpecialValue(Monster monster, ISpell spell)
    {
        return monster.GetSpells()[0] == spell 
            ? monster.GetSpecial1() 
            : monster.GetSpecial2();
    }
    
    public static int GetSpecialValue(Monster monster, IPassive passive)
    {
        return monster.GetPassives()[0] == passive && monster.GetPassives().Count == 1
            ? monster.GetSpecial1()
            : monster.GetSpecial2();
    }

    public static ISpell GetSpell(Monster monster, string spellName)
    {
        if(spellName == monster.GetAutoAttack().GetName())
        {
            return monster.GetAutoAttack();
        }
        if(spellName == monster.GetSwap().GetName())
        {
            return monster.GetSwap();
        }

        foreach (var spell in monster.GetSpells())
        {
            if (spell.GetName() == spellName)
            {
                return spell;
            }
        }

        return null;
    }
}