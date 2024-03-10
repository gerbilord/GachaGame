using UnityEngine;
using UnityEngine.EventSystems;

public class PlayTurnClickScript: MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("PlayTurnClickScript.OnPointerClick() was called!");
        GlobalVariables.UiRunner.EndTurn();
    }
}