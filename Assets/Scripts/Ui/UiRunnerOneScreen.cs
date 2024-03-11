using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private Dictionary<GameObject, int> _textDisplayToMonsterId = new Dictionary<GameObject, int>();
    
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
            _monsterIdToTextDisplay[monster.GetId()].GetComponentInChildren<TMP_Text>().text = MonsterToText(monster);
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
        monsterTextUi.name = monster.GetId() + "-" + monster.GetName();
        monsterTextUi.GetComponentInChildren<TMP_Text>().text = MonsterToText(monster);
        _monsterIdToTextDisplay.Add(monster.GetId(), monsterTextUi);
        _textDisplayToMonsterId.Add(monsterTextUi, monster.GetId());
    }

    public void UpdateBoardState(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        AsyncUtils.ForgetAndLog(UpdateBoardWithAnimations(actionResults, playerBoard1, playerBoard2));
    }

    public void MonsterClicked(GameObject monsterTextUi)
    {
        int monsterId = _textDisplayToMonsterId[monsterTextUi];
        Debug.Log("Monster clicked: " + monsterId);
    }

    private async Task UpdateBoardWithAnimations(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        foreach (PlayerActionResult actionResult in actionResults)
        {
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().monsterId], true);
            await Task.Delay(1000);
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().targetId], true);
            await Task.Delay(1000);
            ShowBoardState(actionResult.GetPlayer1BoardSnapshot(), actionResult.GetPlayer2BoardSnapshot());
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().monsterId], false);
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().targetId], false);
            await Task.Delay(1000);
        }

        ShowBoardState(playerBoard1, playerBoard2);
    }

    public void EndTurn()
    {
        List<PlayerAction> player1Actions = new List<PlayerAction>();
        List<PlayerAction> player2Actions = new List<PlayerAction>();
        player1Actions.Add(new PlayerAction(TestUtils.GetRandomMonsterId(uiPlayer1Board), TestUtils.GetRandomMonsterId(uiPlayer2Board), ActionEnum.Attack));
        player2Actions.Add(new PlayerAction(TestUtils.GetRandomMonsterId(uiPlayer2Board), TestUtils.GetRandomMonsterId(uiPlayer1Board), ActionEnum.Attack));

        _serverGameEngine.ReceivePlayerActions(player1Actions, player2Actions);
    }

    private string MonsterToText(Monster monster)
    {
        string text = "";
        text += monster.GetName() + "\n";
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
    
    public void MonsterClicked(GameObject monsterTextUi);
    
    public void EndTurn();
}