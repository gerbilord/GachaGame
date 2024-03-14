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

        List<PossibleAction> possibleActions = monster.GetPossibleActions(GvUi.playerBoard1, GvUi.playerBoard2);

        string action = await GetUserOptionSelect(possibleActions.Select(item=>item.name).ToList());
        
        List<Monster> possibleTargets = possibleActions.First(item => item.name == action).possibleTargets;
        Monster target = null;
        if (possibleTargets.Count > 0)
        {
            target = await GetMonsterSelect(possibleTargets);
        }

        GvUi.ui.altUiRunner = oldAltUi;

        if (target == null)
        {
            return new PlayerAction(monster.GetId(), -1, MonsterUtils.GetSpell(monster, action));
        }

        return new PlayerAction(monster.GetId(), target.GetId(), MonsterUtils.GetSpell(monster, action));
    }
}