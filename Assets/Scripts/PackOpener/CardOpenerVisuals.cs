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
        // TMP_Text text = GetComponentInChildren<TMP_Text>();
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        Dictionary<Stat, TMP_Text> statToText = generateStatToText(texts);

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            CycleRarityLevel(texts);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject explosionPrefabToCopy = explosionPrefabs[UnityEngine.Random.Range(0, explosionPrefabs.Count)];
            GameObject explosion = Instantiate(explosionPrefabToCopy, Vector3.zero, explosionPrefabToCopy.transform.rotation);

            // text.text = BaseCard.GenerateRandomCardWithRarity(rarityLevel).ToString();
            Card card = BaseCard.GenerateRandomCardWithRarity(rarityLevel);
            foreach (var stat in card.stats)
            {
                statToText[stat.Key].text = stat.Value.ToString();
            }
            
            getNameText(texts).text = card.name;
            
            // Destroy the explosion effect after 3 seconds
            Destroy(explosion, 3);
        }
        
    }

    private Dictionary<Stat, TMP_Text> generateStatToText(TMP_Text[] texts)
    {
        Dictionary<Stat, TMP_Text> statToText = new Dictionary<Stat, TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            Stat stat = Stat.Attack;
            if (text.name.Contains("hp"))
            {
                stat = Stat.Hp;
            }
            else if (text.name.Contains("armor"))
            {
                stat = Stat.Armor;
            }
            else if (text.name.Contains("resist"))
            {
                stat = Stat.Resist;
            }
            else if (text.name.Contains("special1"))
            {
                stat = Stat.Special1;
            }
            else if (text.name.Contains("special2"))
            {
                stat = Stat.Special2;
            }
            else if (text.name.Contains("mana"))
            {
                stat = Stat.Mana;
            }
            else if (text.name.Contains("attack"))
            {
                stat = Stat.Attack;
            }
            else
            {
                continue;
            }
            statToText.Add(stat, text);
        }
        return statToText;
    }
    
    private TMP_Text getNameText(TMP_Text[] texts)
    {
        foreach (TMP_Text text in texts)
        {
            if (text.name.Contains("name"))
            {
                return text;
            }
        }
        return null;
    }

    public void CycleRarityLevel(TMP_Text[] texts)
    {
        // get TMPro text component
        TMP_Text text = getNameText(texts);
        
        // cycle rarity level
        rarityLevel = rarityLevel % 8 + 1;
        
        // set text to new rarity level
        text.text = Rarity.GetRarity(rarityLevel).rarityName;
    }
}