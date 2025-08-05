using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardDisplay cardDisplay;
    private GameObject hoverInfoContainerPrefab;
    private GameObject infoCardPrefab;
    
    private GameObject hoverInfoContainerInstance;
    
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
                containerRect.position = rightEdgePos + new Vector3(15, 110, 0);
            }
        }
        
        // Show container
        hoverInfoContainerInstance.SetActive(true);
        
        // Clear existing info cards
        foreach (Transform child in hoverInfoContainerInstance.transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create info cards for each special that exists
        if (cardData.special1 != null && cardData.stats[Stat.Special1] > 0)
        {
            GameObject special1InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
            string special1Text = FormatSpecialText(cardData.special1, cardData.stats[Stat.Special1]);
            SetupInfoCard(special1InfoCard, special1Text);
        }
        
        if (cardData.special2 != null && cardData.stats[Stat.Special2] > 0)
        {
            GameObject special2InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
            string special2Text = FormatSpecialText(cardData.special2, cardData.stats[Stat.Special2]);
            SetupInfoCard(special2InfoCard, special2Text);
        }
        
        if (cardData.special3 != null && cardData.stats[Stat.Special3] > 0)
        {
            GameObject special3InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
            string special3Text = FormatSpecialText(cardData.special3, cardData.stats[Stat.Special3]);
            SetupInfoCard(special3InfoCard, special3Text);
        }
        
        if (cardData.special4 != null && cardData.stats[Stat.Special4] > 0)
        {
            GameObject special4InfoCard = Instantiate(infoCardPrefab, hoverInfoContainerInstance.transform);
            string special4Text = FormatSpecialText(cardData.special4, cardData.stats[Stat.Special4]);
            SetupInfoCard(special4InfoCard, special4Text);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverInfoContainerInstance != null)
        {
            hoverInfoContainerInstance.SetActive(false);
        }
    }
    
    private void SetupInfoCard(GameObject infoCard, string value)
    {
        // Find text components in the info card
        TextMeshProUGUI[] texts = infoCard.GetComponentsInChildren<TextMeshProUGUI>();
        
        texts[0].text = $"{value}";
    }
    
    private string FormatSpecialText(Special special, int level)
    {
        int manaCost = special.manaMultiplier * level;
        string manaText = manaCost > 0 ? $" - {manaCost} Mana" : "";
        return $"{special.name} \n (Lv.{level}){manaText}\n\n{special.description}";
    }
}