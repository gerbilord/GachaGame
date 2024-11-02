public class Rarity
{
    public static Rarity RARITY1 = new Rarity("Common", 7);
    public static Rarity RARITY2 = new Rarity("Uncommon",  10);
    public static Rarity RARITY3 = new Rarity("Rare",  15);
    public static Rarity RARITY4 = new Rarity("Epic",  20);
    public static Rarity RARITY5 = new Rarity("Legendary",  25);
    public static Rarity RARITY6 = new Rarity("Mythical", 30);
    public static Rarity RARITY7 = new Rarity("Unheard of", 35);
    public static Rarity RARITY8 = new Rarity("Literally hacking", 40);

    public string rarityName { get; private set; }
    public int rarityStats { get; private set; }
    
    public Rarity(string rarityName, int rarityStats)
    {
        this.rarityName = rarityName;
        this.rarityStats = rarityStats;
    }

    public static Rarity GetRarity(int rarityLevel)
    {
        switch(rarityLevel)
        {
            case 1:
                return RARITY1;
            case 2:
                return RARITY2;
            case 3:
                return RARITY3;
            case 4:
                return RARITY4;
            case 5:
                return RARITY5;
            case 6:
                return RARITY6;
            case 7:
                return RARITY7;
            case 8:
                return RARITY8;
            default:
                return RARITY1;
        }
    }
}


