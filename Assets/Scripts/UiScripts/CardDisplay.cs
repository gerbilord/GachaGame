using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{
    [Header("Text Elements")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI resistText;
    public TextMeshProUGUI special1Text;
    public TextMeshProUGUI special2Text;
    public TextMeshProUGUI special3Text;
    public TextMeshProUGUI special4Text;
    public TextMeshProUGUI manaText;
    
    [Header("Visual Indicators")]
    public GameObject magicIndicator;
    public List<GameObject> nightmareIndicators;
    public RawImage background;

    public void SetCardData(CardData cardData)
    {
        if (titleText != null)
            titleText.text = $"{(cardData.isNightmare ? "Nightmarish " : "")}" + $"{Rarity.GetRarity(cardData.rarityLevel).rarityName} " + $"{cardData.creatureType}";

        if (attackText != null)
            attackText.text = cardData.stats[Stat.Attack].ToString();

        if (hpText != null)
            hpText.text = cardData.stats[Stat.Hp].ToString();

        if (armorText != null)
        {
            armorText.text = cardData.stats[Stat.Armor].ToString();
            armorText.gameObject.SetActive(!cardData.isNightmare);
        }

        if (resistText != null)
        {
            resistText.text = cardData.stats[Stat.Resist].ToString();
            resistText.gameObject.SetActive(!cardData.isNightmare);
        }

        if (special1Text != null)
            special1Text.text = cardData.stats[Stat.Special1].ToString();

        if (special2Text != null)
            special2Text.text = cardData.stats[Stat.Special2].ToString();

        if (special3Text != null)
        {
            special3Text.text = cardData.stats[Stat.Special3].ToString();
            special3Text.gameObject.SetActive(cardData.isNightmare);
        }


        if (special4Text != null)
        {
            special4Text.text = cardData.stats[Stat.Special4].ToString();
            special4Text.gameObject.SetActive(cardData.isNightmare);
        }


        if (manaText != null)
            manaText.text = cardData.stats[Stat.Mana].ToString();

        if (magicIndicator != null)
            magicIndicator.SetActive(cardData.isMagic);

        if (nightmareIndicators != null)
            nightmareIndicators.ForEach(go => { go.SetActive(cardData.isNightmare); });


        if (background != null)
        {
            // Set background color based on rarity
            // define colors for each rarity level
            Color rarityColor;
            switch (cardData.rarityLevel)
            {
                case 1:
                    rarityColor = Color.black; // Common
                    break;
                case 2:
                    rarityColor = Color.gray; // Uncommon
                    break;
                case 3:
                    rarityColor = new Color(.1f, .1f, 0.8f); // Rare (dark blue)
                    break;
                case 4:
                    rarityColor = new Color(0.8f, .3f, 0.8f); // Epic (purple)
                    break;
                case 5:
                    rarityColor = Color.red; // Legendary
                    break;
                case 6:
                    rarityColor = new Color(1f, 0.5f, 0); // Mythic (orange)
                    break;
                case 7:
                    rarityColor = new Color(0.75f, 0.75f, 0); // Divine (gold)
                    break;
                case 8:
                    rarityColor = Color.cyan;
                    break;
                default:
                    rarityColor = Color.magenta; // Default color for unknown rarity
                    break;
            }
            background.color = rarityColor;
        }
    }
}