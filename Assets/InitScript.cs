using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
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
        for (int i = 0; i < 3; i++)
        {
            int cardsGenerated = 1;
            int maxCards = 7; // Maximum cards to generate
            while (cardsGenerated <= maxCards)
            {
                CardData card = CardGenerator.GenerateCard();

                if (cardsGenerated > 5 && card.rarityLevel == 1)
                {
                    continue; // Skip low rarity cards after 5 cards have been generated
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
