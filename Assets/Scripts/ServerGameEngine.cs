using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerGameEngine : MonoBehaviour
{
    private IUiRunner _uiRunner;

    private PlayerBoard _player1Board;
    private PlayerBoard _player2Board;
    
    private List<PlayerAction> _player1Actions;
    private List<PlayerAction> _player2Actions;

    void Start()
    {
        
        GameObject canvas = GameObject.Find("Canvas");
        _uiRunner = canvas.GetComponent<UiRunnerOneScreen>();
        GlobalVariables.UiRunner = _uiRunner;

        _player1Board = new PlayerBoard(TestUtils.CreateHunter(), TestUtils.CreateMonsters(), new());
        _player2Board = new PlayerBoard(TestUtils.CreateHunter(), TestUtils.CreateMonsters(), new());
        
        _uiRunner.OnEngineStart(this, _player1Board, _player2Board);
        _uiRunner.ShowBoardState(_player1Board, _player2Board);
    }
    
    public void ReceivePlayerActions(List<PlayerAction> player1Actions, List<PlayerAction> player2Actions)
    {
        _player1Actions = player1Actions;
        _player2Actions = player2Actions;

        List<PlayerActionResult> actionResults = RunPlayerActions();
        
        _uiRunner.UpdateBoardState(actionResults,_player1Board, _player2Board);
    }

    private List<PlayerActionResult> RunPlayerActions()
    {
        List<PlayerActionResult> actionResults = new List<PlayerActionResult>();

        for (int i = 0; i < _player1Actions.Count; i++)
        {
            PlayerAction player1Action = _player1Actions[i];
            // PlayerAction player2Action = _player2Actions[i];
            
            PlayerActionResult player1Result = RunPlayerAction(player1Action);
            // PlayerActionResult player2Result = RunPlayerAction(player2Action);
            
            actionResults.Add(player1Result);
            // actionResults.Add(player2Result);
        }

        return actionResults;
    }

    private PlayerActionResult RunPlayerAction(PlayerAction playerAction)
    {
        playerAction.spell.Cast(GetMonsterById(playerAction.monsterId), GetMonsterById(playerAction.targetId), _player1Board, _player2Board);
        return new PlayerActionResult(playerAction, _player1Board.DeepCopy(), _player2Board.DeepCopy());
    }
    

    private PlayerActionResult RunAttackAction(PlayerAction playerAction)
    {
        Monster attacker = GetMonsterById(playerAction.monsterId);
        Monster target = GetMonsterById(playerAction.targetId);

        int damage = attacker.GetAttack();
        target.TakeDamage(damage, null);

        return new PlayerActionResult(playerAction, _player1Board.DeepCopy(), _player2Board.DeepCopy());
    }

    private Monster GetMonsterById(int id)
    {
        if(id == -1)
            return null;

        return _player1Board.GetMonsters().Find(monster => monster.GetId() == id) 
               ?? _player2Board.GetMonsters().Find(monster => monster.GetId() == id);
    }
}
