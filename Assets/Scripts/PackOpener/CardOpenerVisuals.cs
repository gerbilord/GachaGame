using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardOpenerVisuals : MonoBehaviour, IPointerClickHandler
{
    int rarityLevel = 1;
    List<GameObject> explosionPrefabs = new List<GameObject>();

    private void OnEnable()
    {
        // Load all prefabs from folder /Explosions
        explosionPrefabs = Resources.LoadAll<GameObject>("Prefabs/ExplosionEffects").ToList();
    }

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
            GameObject explosionPrefabToCopy = explosionPrefabs[UnityEngine.Random.Range(0, explosionPrefabs.Count)];
            GameObject explosion = Instantiate(explosionPrefabToCopy, Vector3.zero, explosionPrefabToCopy.transform.rotation);

            text.text = BaseCard.GenerateRandomCardWithRarity(rarityLevel).ToString();
            
            // Destroy the explosion effect after 3 seconds
            Destroy(explosion, 3);
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