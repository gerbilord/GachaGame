public static class MonsterUtils
{
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