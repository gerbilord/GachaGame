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
            // Find the topmost canvas to ensure hover info is always on top
            Canvas topCanvas = GetComponentInParent<Canvas>();
            Transform parentTransform = topCanvas != null ? topCanvas.transform : transform;
            
            hoverInfoContainerInstance = Instantiate(hoverInfoContainerPrefab, parentTransform);
            
            // Ensure it's the last child (renders on top)
            hoverInfoContainerInstance.transform.SetAsLastSibling();
            
            // Add Canvas component to ensure proper sorting
            Canvas hoverCanvas = hoverInfoContainerInstance.GetComponent<Canvas>();
            if (hoverCanvas == null)
            {
                hoverCanvas = hoverInfoContainerInstance.AddComponent<Canvas>();
            }
            hoverCanvas.overrideSorting = true;
            hoverCanvas.sortingOrder = 1000; // High value to ensure it's on top
            
            // Add GraphicRaycaster if needed for interaction
            if (hoverInfoContainerInstance.GetComponent<UnityEngine.UI.GraphicRaycaster>() == null)
            {
                hoverInfoContainerInstance.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
            
            // Position it relative to the card
            RectTransform containerRect = hoverInfoContainerInstance.GetComponent<RectTransform>();
            RectTransform cardRect = GetComponent<RectTransform>();
            if (containerRect != null && cardRect != null)
            {
                // Get world position of the card's right edge
                Vector3[] cardCorners = new Vector3[4];
                cardRect.GetWorldCorners(cardCorners);
                Vector3 rightEdgePos = (cardCorners[2] + cardCorners[3]) / 2f;
                
                // Convert to local position in parent canvas
                containerRect.position = rightEdgePos + new Vector3(10, 0, 0);
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