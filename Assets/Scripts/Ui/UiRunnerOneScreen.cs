using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Purchasing;
using UnityEngine;

public class UiRunnerOneScreen : MonoBehaviour, IUiRunner
{
    private ServerGameEngine _serverGameEngine;
    
    public GameObject player1HierarchyParent;
    public GameObject player2HierarchyParent;
    
    public GameObject monsterTextUiPrefab;

    private PlayerBoard uiPlayer1Board;
    private PlayerBoard uiPlayer2Board;
    
    private Dictionary<int, GameObject> _monsterIdToTextDisplay = new Dictionary<int, GameObject>();
    
    

    public void OnEngineStart(ServerGameEngine serverGameEngine, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        _serverGameEngine = serverGameEngine;
        initalizeAllMonsterUi(playerBoard1, playerBoard2);
        ShowBoardState(playerBoard1, playerBoard2);
    }
    

    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        uiPlayer1Board = playerBoard1;
        uiPlayer2Board = playerBoard2;
        
        playerBoard1.GetMonsters().Concat(playerBoard2.GetMonsters()).ToList().ForEach(monster =>
        {
            _monsterIdToTextDisplay[monster.GetId()].GetComponent<TMP_Text>().text = MonsterToText(monster);
        });
    }

    private void initalizeAllMonsterUi(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        playerBoard1.GetMonsters().ForEach(monster=>{initalizeMonsterUi(monster, player1HierarchyParent);});
        playerBoard2.GetMonsters().ForEach(monster=>{initalizeMonsterUi(monster, player2HierarchyParent);});
    }

    private void initalizeMonsterUi(Monster monster, GameObject parent)
    {
        GameObject monsterTextUi = Instantiate(monsterTextUiPrefab, parent.transform);
        monsterTextUi.GetComponent<TMP_Text>().text = MonsterToText(monster);
        _monsterIdToTextDisplay.Add(monster.GetId(), monsterTextUi);
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

        _serverGameEngine.ReceivePlayerActions(player1Actions, player2Actions);
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
    public void OnEngineStart(ServerGameEngine serverGameEngine, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    public void UpdateBoardState(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    
    public void EndTurn();
}