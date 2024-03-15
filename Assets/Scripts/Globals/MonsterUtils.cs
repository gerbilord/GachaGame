public static class MonsterUtils
{
    public static ISpell GetSpell(Monster monster, string spellName)
    {
        // TODO Make default spells area
        if(spellName == "AutoAttack")
        {
            return new AutoAttack();
        }
        else if(spellName == "Swap")
        {
            return new Swap();
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