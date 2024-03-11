using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterClickScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalVariables.UiRunner.MonsterClicked(gameObject);
    }
}