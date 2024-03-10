using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameEngine : MonoBehaviour
{
    private IUiRunner _uiRunner;

    private PlayerBoard _player1;
    private PlayerBoard _player2;
    
    private List<PlayerAction> _player1Actions;
    private List<PlayerAction> _player2Actions;

    void Start()
    {
        _uiRunner = new UiRunnerOneScreen(this);
        GlobalVariables.UiRunner = _uiRunner;

        
        _player1 = new PlayerBoard(TestUtils.CreateHunter(), TestUtils.CreateMonsters());
        _player2 = new PlayerBoard(TestUtils.CreateHunter(), TestUtils.CreateMonsters());
        
        _uiRunner.ShowBoardState(_player1, _player2);
    }
    
    public void RecievePlayerActions(List<PlayerAction> player1Actions, List<PlayerAction> player2Actions)
    {
        Debug.Log("RecievePlayerActions() was called!");
        _player1Actions = player1Actions;
        _player2Actions = player2Actions;

        List<PlayerActionResult> actionResults = RunPlayerActions();
        
        _uiRunner.UpdateBoardState(actionResults,_player1, _player2);
    }

    private List<PlayerActionResult> RunPlayerActions()
    {
        List<PlayerActionResult> actionResults = new List<PlayerActionResult>();

        for (int i = 0; i < _player1Actions.Count; i++)
        {
            PlayerAction player1Action = _player1Actions[i];
            PlayerAction player2Action = _player2Actions[i];
            
            PlayerActionResult player1Result = RunPlayerAction(player1Action);
            PlayerActionResult player2Result = RunPlayerAction(player2Action);
            
            actionResults.Add(player1Result);
            actionResults.Add(player2Result);
        }

        return actionResults;
    }

    private PlayerActionResult RunPlayerAction(PlayerAction playerAction)
    {
        if (playerAction.actionEnum == ActionEnum.Attack)
        {
            return RunAttackAction(playerAction);
        }
        else if (playerAction.actionEnum == ActionEnum.Spell)
        {
            return RunCastSpellAction(playerAction);
        }

        Debug.Log("WARNING: Unhandled action type: " + playerAction.actionEnum);
        throw new System.NotImplementedException();
    }

    private PlayerActionResult RunCastSpellAction(PlayerAction playerAction)
    {
        throw new System.NotImplementedException();
    }

    private PlayerActionResult RunAttackAction(PlayerAction playerAction)
    {
        Monster attacker = GetMonsterById(playerAction.monsterId);
        Monster target = GetMonsterById(playerAction.targetId);

        int damage = attacker.GetAttack();
        target.TakeDamage(damage, null);
        
        return new PlayerActionResult(playerAction);
    }

    private Monster GetMonsterById(int id)
    {
        return _player1.GetMonsters().Find(monster => monster.GetId() == id) 
               ?? _player2.GetMonsters().Find(monster => monster.GetId() == id);
    }
}
