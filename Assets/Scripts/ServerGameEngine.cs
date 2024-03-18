using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerGameEngine : MonoBehaviour
{
    private IUiFrontendReceiver _uiRunner;

    private PlayerBoard _player1Board;
    private PlayerBoard _player2Board;
    
    private List<PlayerAction> _player1Actions;
    private List<PlayerAction> _player2Actions;
    
    private List<Monster> GetAllMonsters()
    {
        return _player1Board.GetMonsters().Concat(_player2Board.GetMonsters()).ToList();
    }

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        _uiRunner = canvas.GetComponent<UiRunnerOneScreen>();
        GvUi.ui = _uiRunner as IUiRunner;

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
            PlayerAction player2Action = _player2Actions[i];
            
            PlayerAction truePlayer1Action = RunPlayerAction(player1Action);
            actionResults.Add(new PlayerActionResult(truePlayer1Action, _player1Board.DeepCopy(), _player2Board.DeepCopy()));
            
            PlayerAction truePlayer2Action = RunPlayerAction(player2Action);
            actionResults.Add(new PlayerActionResult(truePlayer2Action, _player1Board.DeepCopy(), _player2Board.DeepCopy()));
            
            SendDeadMonstersToGraveyard();
        }

        return actionResults;
    }

    private void SendDeadMonstersToGraveyard()
    {
        foreach (var monster in _player1Board.GetMonsters())
        {
            if (monster.GetHealth() <= 0)
                _player1Board.SendMonsterToGraveyard(monster);
            
        }
        
        foreach (var monster in _player2Board.GetMonsters())
        {
            if(monster.GetHealth() <= 0)
                _player2Board.SendMonsterToGraveyard(monster);
        }
    }

    private PlayerAction RunPlayerAction(PlayerAction playerAction)
    {
        return playerAction.spell.Cast(playerAction, _player1Board, _player2Board);
    }

    private Monster GetMonsterById(int id)
    {
        if(id == -1)
            return null;

        return _player1Board.GetMonsters().Find(monster => monster.GetId() == id) 
               ?? _player2Board.GetMonsters().Find(monster => monster.GetId() == id);
    }

    private void DoForBothBoards(Action<PlayerBoard> action)
    {
        action(_player1Board);
        action(_player2Board);
    }
}
