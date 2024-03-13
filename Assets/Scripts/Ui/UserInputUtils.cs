using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UserInputUtils
{
    public static async Task<string> GetUserOptionSelect(List<string> options)
    {
        IAltUiRunner oldAltUi = GlobalVariables.UiRunner.altUiRunner;
        AltUiMenuSelect altUi = new AltUiMenuSelect();
        GlobalVariables.UiRunner.altUiRunner = altUi;
        string input = await altUi.GetUserOptionSelect(options);
        GlobalVariables.UiRunner.altUiRunner = oldAltUi;
        return input;
    }
    
    public static async Task<Monster> GetMonsterSelect(List<Monster> monsters)
    {
        IAltUiRunner oldAltUi = GlobalVariables.UiRunner.altUiRunner;
        AltUiMonsterSelect altUi = new AltUiMonsterSelect();
        GlobalVariables.UiRunner.altUiRunner = altUi;

        Monster monster = await altUi.GetUserMonsterSelect(monsters);

        GlobalVariables.UiRunner.altUiRunner = oldAltUi;
        return monster;
    }
    
    public static async Task<PlayerAction> GetPlayerAction(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        IAltUiRunner oldAltUi = GlobalVariables.UiRunner.altUiRunner;
        AltUiMenuSelect altUi = new AltUiMenuSelect();
        GlobalVariables.UiRunner.altUiRunner = altUi;

        List<PossibleAction> possibleActions = monster.GetPossibleActions(playerBoard1, playerBoard2);

        string action = await GetUserOptionSelect(possibleActions.Select(item=>item.name).ToList());
        
        List<Monster> possibleTargets = possibleActions.First(item => item.name == action).possibleTargets;
        Monster target = null;
        if (possibleTargets.Count > 0)
        {
            target = await GetMonsterSelect(possibleTargets);
        }

        GlobalVariables.UiRunner.altUiRunner = oldAltUi;

        if (target == null)
        {
            return new PlayerAction(monster.GetId(), -1, MonsterUtils.GetSpell(monster, action));
        }

        return new PlayerAction(monster.GetId(), target.GetId(), MonsterUtils.GetSpell(monster, action));
    }
}