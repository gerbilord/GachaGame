using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardOpenerVisuals : MonoBehaviour, IPointerClickHandler
{
    int rarityLevel = 1;

    public void OnPointerClick(PointerEventData eventData)
    {
        // get TMPro text component
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            CycleRarityLevel();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            text.text = BaseCard.GenerateRandomCardWithRarity(rarityLevel).ToString();
        }
        
    }
    
    public void CycleRarityLevel()
    {
        // get TMPro text component
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        
        // cycle rarity level
        rarityLevel = rarityLevel % 8 + 1;
        
        // set text to new rarity level
        text.text = Rarity.GetRarity(rarityLevel).rarityName;
    }
}