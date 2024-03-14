using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Purchasing;
using UnityEngine;

public class UiRunnerOneScreen : MonoBehaviour, IUiRunner, IUiFrontendReceiver
{
    private ServerGameEngine _serverGameEngine;
    
    public IAltUiRunner altUiRunner { get; set; }

    public GameObject player1HierarchyParent;
    public GameObject player2HierarchyParent;
    
    public GameObject player1GraveyardHierarchyParent;
    public GameObject player2GraveyardHierarchyParent;
    
    public GameObject MenuGameObject;

    public GameObject monsterTextUiPrefab;

    private Dictionary<int, GameObject> _monsterIdToTextDisplay = new Dictionary<int, GameObject>();
    private Dictionary<GameObject, int> _textDisplayToMonsterId = new Dictionary<GameObject, int>();
    
    public GameObject GetMenuGameObject()
    {
        return MenuGameObject;
    }
    
    public void OnEngineStart(ServerGameEngine serverGameEngine, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        _serverGameEngine = serverGameEngine;
        SetGlobalVariables(playerBoard1, playerBoard2);
        initalizeAllMonsterUi();
        ShowBoardState(playerBoard1, playerBoard2);
    }

    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        GvUi.GetAllMonsters().ToList().ForEach(monster =>
        {
            _monsterIdToTextDisplay[monster.GetId()].GetComponentInChildren<TMP_Text>().text = MonsterToText(monster);
        });

        GvUi.DoForBothBoards(board =>
        {
            board.GetGraveyard().ForEach(monster =>
            {
                _monsterIdToTextDisplay[monster.GetId()].GetComponent<RectTransform>().localScale = new Vector3(.3f, .3f, .3f);
                _monsterIdToTextDisplay[monster.GetId()].transform.SetParent(GetGraveyardHierarchyParent(board).transform, false);
            });
        });
    }

    private void initalizeAllMonsterUi()
    {
        GvUi.DoForBothBoards(board =>
            {
                board.GetMonsters().ForEach(monster=>{initalizeMonsterUi(monster, GetHierarchyParent(board));});
            });
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
        SetGlobalVariables(playerBoard1, playerBoard2);
        AsyncUtils.ForgetAndLog(UpdateBoardWithAnimations(actionResults));
    }

    public void MonsterClicked(GameObject monsterTextUi)
    {
        int monsterId = _textDisplayToMonsterId[monsterTextUi];
        Monster monster = GvUi.GetAllMonsters().Find(m => m.GetId() == monsterId);
        Debug.Log("Monster clicked: " + monsterId);
        
        if(altUiRunner != null)
        {
            altUiRunner.OnMonsterClicked(monster);
            return;
        }

        AsyncUtils.ForgetAndLog(GetUserAction(monster));
    }

    public async Task GetUserAction(Monster monster)
    {
        PlayerAction playerAction = await UserInputUtils.GetPlayerAction(monster);
        _serverGameEngine.ReceivePlayerActions(new List<PlayerAction>(){playerAction}, new());
    }
    
    public void OptionClicked(GameObject option)
    {
        if(altUiRunner != null)
        {
            altUiRunner.OnOptionClicked(option);
        }
    }

    private async Task UpdateBoardWithAnimations(List<PlayerActionResult> actionResults)
    {
        foreach (PlayerActionResult actionResult in actionResults)
        {
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().monsterId], true);
            await Task.Delay(1000);
            if (actionResult.GetPlayerAction().targetId != -1)
            {
                HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().targetId], true);
                await Task.Delay(1000);
            }
            ShowBoardState(actionResult.GetPlayer1BoardSnapshot(), actionResult.GetPlayer2BoardSnapshot());
            HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().monsterId], false);
            if (actionResult.GetPlayerAction().targetId != -1)
            {
                HighlighterUtils.ToggleHighlight(_monsterIdToTextDisplay[actionResult.GetPlayerAction().targetId], false);
            }
            await Task.Delay(1000);
        }

        ShowBoardState(GvUi.playerBoard1, GvUi.playerBoard2);
    }

    private string MonsterToText(Monster monster)
    {
        string text = "";
        text += monster.GetName() + "\n";
        text += "\t" + "Attack: " + monster.GetAttack() + "\n";
        text += "\t" + "Health: " + monster.GetHealth() + "\n";
        return text;
    }
    
    private void SetGlobalVariables(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        GvUi.playerBoard1 = playerBoard1;
        GvUi.playerBoard2 = playerBoard2;
    }

    private GameObject GetHierarchyParent(PlayerBoard playerBoard)
    {
        if (GvUi.playerBoard1 == playerBoard)
        {
            return player1HierarchyParent;
        }
        
        return player2HierarchyParent;
    }
    
    private GameObject GetGraveyardHierarchyParent(PlayerBoard playerBoard)
    {
        if (GvUi.playerBoard1 == playerBoard)
        {
            return player1GraveyardHierarchyParent;
        }
        
        return player2GraveyardHierarchyParent;
    }

}

public interface IUiFrontendReceiver
{
    public void OnEngineStart(ServerGameEngine serverGameEngine, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    public void UpdateBoardState(List<PlayerActionResult> actionResults, PlayerBoard playerBoard1, PlayerBoard playerBoard2);
    public void ShowBoardState(PlayerBoard playerBoard1, PlayerBoard playerBoard2);
}
public interface IUiRunner
{
    public void MonsterClicked(GameObject monsterTextUi);
    
    public void OptionClicked(GameObject option);
    // public void EndTurn();
    
    public IAltUiRunner altUiRunner { get; set; }
    
    public GameObject GetMenuGameObject();
}