using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiRunnerOneScreen : IUiRunner
{
    private ServerGameEngine _serverGameEngine;
    private GameObject player1TextUi;
    private GameObject player2TextUi;
    
    private PlayerBoard uiPlayer1Board;
    private PlayerBoard uiPlayer2Board;

    public UiRunnerOneScreen(ServerGameEngine serverGameEngine)
    {
        _serverGameEngine = serverGameEngine;
        player1TextUi = GameObject.Find("Player1 Text Ui");
        player2TextUi = GameObject.Find("Player2 Text Ui");
    }

    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        uiPlayer1Board = playerBoard1;
        uiPlayer2Board = playerBoard2;
        
        player1TextUi.GetComponent<TMP_Text>().text = PlayerBoardToText(uiPlayer1Board);
        player2TextUi.GetComponent<TMP_Text>().text = PlayerBoardToText(uiPlayer2Board);
    }

    public void UpdateBoardState(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        ShowBoardState(playerBoard1, playerBoard2);
    }

    public void EndTurn()
    {
        Debug.Log("endturn was called!");
        List<PlayerAction> player1Actions = new List<PlayerAction>();
        List<PlayerAction> player2Actions = new List<PlayerAction>();
        player1Actions.Add(new PlayerAction(TestUtils.GetRandomMonsterId(uiPlayer1Board), TestUtils.GetRandomMonsterId(uiPlayer2Board), ActionEnum.Attack));
        player2Actions.Add(new PlayerAction(TestUtils.GetRandomMonsterId(uiPlayer2Board), TestUtils.GetRandomMonsterId(uiPlayer1Board), ActionEnum.Attack));

        _serverGameEngine.RecievePlayerActions(player1Actions, player2Actions);
    }

    private string PlayerBoardToText(PlayerBoard playerBoard)
    {
        string text = "";

        foreach (Monster monster in playerBoard.GetMonsters())
        {
            text += MonsterToText(monster) + "\n";
        }

        return text;
    }

    private string MonsterToText(Monster monster)
    {
        string text = "";
        text += "Name: " + monster.GetName() + "\n";
        text += "\t" + "Attack: " + monster.GetAttack() + "\n";
        text += "\t" + "Health: " + monster.GetHealth() + "\n";
        return text;
    }
}

public interface IUiRunner
{
    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    public void UpdateBoardState(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    public void EndTurn();
}