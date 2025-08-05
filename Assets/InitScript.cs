using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
    // Configuration constants
    private const int PACK_COUNT = 3;                // Number of packs to generate
    private const int CARDS_PER_PACK = 7;            // Number of cards in each pack
    private const int NUMBER_OF_CARDS_TO_GAURANTEE_UPGRADE = 2; // This many cards will be guaranteed to be of at least this rarity
    private const int MIN_RARITY_FOR_GUARANTEED = 1;  // Minimum rarity level for guaranteed cards
    
    public GameObject libraryGrid;
    public GameObject cardPrefab;
    
    private List<GameObject> currentCards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RegenerateCards();
        }
    }
    
    void GenerateCards()
    {
        for (int i = 0; i < PACK_COUNT; i++)
        {
            int cardsGenerated = 1;
            while (cardsGenerated <= CARDS_PER_PACK)
            {
                CardData card = CardGenerator.GenerateCard();

                if (cardsGenerated > CARDS_PER_PACK - NUMBER_OF_CARDS_TO_GAURANTEE_UPGRADE && card.rarityLevel == MIN_RARITY_FOR_GUARANTEED)
                {
                    continue; // Skip low rarity cards after threshold
                }
                // Instantiate card prefab
                GameObject cardObject = Instantiate(cardPrefab, libraryGrid.transform);
                currentCards.Add(cardObject);
                
                // Get CardDisplay component and set data
                CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
                if (cardDisplay != null)
                {
                    cardDisplay.SetCardData(card);
                }
                else
                {
                    Debug.LogError("CardDisplay component not found on card prefab!");
                }
                cardsGenerated++;
            }
        }
    }
    
    void RegenerateCards()
    {
        // Destroy all existing cards
        foreach (GameObject card in currentCards)
        {
            Destroy(card);
        }
        currentCards.Clear();
        
        // Generate new cards
        GenerateCards();
    }
}
