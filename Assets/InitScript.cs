using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
    public GameObject libraryGrid;
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            CardData card = CardGenerator.GenerateCard();
            
            // Instantiate card prefab
            GameObject cardObject = Instantiate(cardPrefab, libraryGrid.transform);
            
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
