using UnityEngine;
using UnityEngine.EventSystems;

public class MenuOptionClickScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GvUi.ui.OptionClicked(gameObject);
    }
}