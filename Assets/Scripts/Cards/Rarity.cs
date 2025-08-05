public class Rarity
{
    public static Rarity RARITY1 = new Rarity("Common", 7);
    public static Rarity RARITY2 = new Rarity("Uncommon", 14);
    public static Rarity RARITY3 = new Rarity("Rare", 20);
    public static Rarity RARITY4 = new Rarity("Epic", 26);
    public static Rarity RARITY5 = new Rarity("Legendary", 31);
    public static Rarity RARITY6 = new Rarity("Mythical", 36);
    public static Rarity RARITY7 = new Rarity("Unheard of", 44);
    public static Rarity RARITY8 = new Rarity("Literally hacking", 48);

    public string rarityName { get; private set; }
    public int extraStats { get; private set; }
    
    private Rarity(string rarityName, int extraStats)
    {
        this.rarityName = rarityName;
        this.extraStats = extraStats;
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
