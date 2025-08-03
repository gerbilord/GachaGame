using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            CardData card = CardGenerator.GenerateCard();
            Debug.Log(card.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
