using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardDisplay cardDisplay;
    private GameObject hoverInfoContainerPrefab;
    private GameObject infoCardPrefab;
    
    private GameObject hoverInfoContainerInstance;
    private GameObject special1InfoCard;
    private GameObject special2InfoCard;
    
    void Start()
    {
        cardDisplay = GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            hoverInfoContainerPrefab = cardDisplay.hoverInfoContainerPrefab;
            infoCardPrefab = cardDisplay.hoverInfoItemPrefab;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverInfoContainerPrefab == null || infoCardPrefab == null || cardDisplay == null) return;
        
        CardData cardData = cardDisplay.GetCardData();
        if (cardData == null) return;
        
        // Create container instance if it doesn't exist
        if (hoverInfoContainerInstance == null)
        {
            hoverInfoContainerInstance = Instantiate(hoverInfoContainerPrefab, transform);
            // Position it to the right of the card
            RectTransform containerRect = hoverInfoContainerInstance.GetComponent<RectTransform>();
            if (containerRect != null)
            {
                containerRect.anchorMin = new Vector2(1, 0.5f);
                containerRect.anchorMax = new Vector2(1, 0.5f);
                containerRect.pivot = new Vector2(0, 0.5f);
                containerRect.anchoredPosition = new Vector2(10, 0);
            }
        }
        
        // Show container
        hoverInfoContainerInstance.SetActive(true);
        
        // Clear existing info cards
        foreach (Transform child in hoverInfoContainerInstance.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create Special1 info card
        special1InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
        SetupInfoCard(special1InfoCard, "Special 1", cardData.stats[Stat.Special1].ToString());
        
        // Create Special2 info card
        special2InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
        SetupInfoCard(special2InfoCard, "Special 2", cardData.stats[Stat.Special2].ToString());
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverInfoContainerInstance != null)
        {
            hoverInfoContainerInstance.SetActive(false);
        }
    }
    
    private void SetupInfoCard(GameObject infoCard, string title, string value)
    {
        // Find text components in the info card
        TextMeshProUGUI[] texts = infoCard.GetComponentsInChildren<TextMeshProUGUI>();
        
        if (texts.Length >= 2)
        {
            texts[0].text = title;
            texts[1].text = value;
        }
        else if (texts.Length == 1)
        {
            texts[0].text = $"{title}: {value}";
        }
    }
}