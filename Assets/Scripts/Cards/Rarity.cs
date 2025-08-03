public class Rarity
{
    public static Rarity RARITY1 = new Rarity("Common");
    public static Rarity RARITY2 = new Rarity("Uncommon");
    public static Rarity RARITY3 = new Rarity("Rare");
    public static Rarity RARITY4 = new Rarity("Epic");
    public static Rarity RARITY5 = new Rarity("Legendary");
    public static Rarity RARITY6 = new Rarity("Mythical");
    public static Rarity RARITY7 = new Rarity("Unheard of");
    public static Rarity RARITY8 = new Rarity("Literally hacking");

    public string rarityName { get; private set; }
    
    private Rarity(string rarityName)
    {
        this.rarityName = rarityName;
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
