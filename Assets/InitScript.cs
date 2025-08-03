using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CardGenerator cardGenerator = new CardGenerator();
        CardData card = cardGenerator.generateCardDataForRarity(0);
        
        Debug.Log(card.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
