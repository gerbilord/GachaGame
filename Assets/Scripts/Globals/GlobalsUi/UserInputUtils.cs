using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UserInputUtils
{
    public static async Task<string> GetUserOptionSelect(List<string> options)
    {
        IAltUiRunner oldAltUi = GvUi.ui.altUiRunner;
        AltUiMenuSelect altUi = new AltUiMenuSelect();
        GvUi.ui.altUiRunner = altUi;
        string input = await altUi.GetUserOptionSelect(options);
        GvUi.ui.altUiRunner = oldAltUi;
        return input;
    }
    
    public static async Task<Monster> GetMonsterSelect(List<Monster> monsters)
    {
        IAltUiRunner oldAltUi = GvUi.ui.altUiRunner;
        AltUiMonsterSelect altUi = new AltUiMonsterSelect();
        GvUi.ui.altUiRunner = altUi;

        Monster monster = await altUi.GetUserMonsterSelect(monsters);

        GvUi.ui.altUiRunner = oldAltUi;
        return monster;
    }
    
    public static async Task<PlayerAction> GetPlayerAction(Monster monster)
    {
        IAltUiRunner oldAltUi = GvUi.ui.altUiRunner;
        AltUiMenuSelect altUi = new AltUiMenuSelect();
        GvUi.ui.altUiRunner = altUi;
        
        List<ISpell> castableSpells = monster.GetCastableSpells(GvUi.playerBoard1, GvUi.playerBoard2);

        string spellName = await GetUserOptionSelect(castableSpells.Select(spell => spell.GetName()).ToList());
        
        List<Monster> possibleTargets = castableSpells.First(spell => spell.GetName() == spellName).GetPossibleTargets(monster, GvUi.playerBoard1, GvUi.playerBoard2);
        Monster target = null;
        if (possibleTargets.Count > 0)
        {
            target = await GetMonsterSelect(possibleTargets);
        }

        GvUi.ui.altUiRunner = oldAltUi;

        if (target == null)
        {
            return new PlayerAction(monster.GetId(), new(), MonsterUtils.GetSpell(monster, spellName));
        }

        return new PlayerAction(monster.GetId(), new List<int>(){target.GetId()}, MonsterUtils.GetSpell(monster, spellName));
    }
}