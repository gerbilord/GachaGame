public static class MonsterUtils
{
    public static ISpell GetSpell(Monster monster, string spellName)
    {
        if(spellName == "AutoAttack")
        {
            return new AutoAttack();
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