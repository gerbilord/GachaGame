using UnityEngine;
using UnityEngine.EventSystems;

public class MenuOptionClickScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalVariables.UiRunner.OptionClicked(gameObject);
    }
}