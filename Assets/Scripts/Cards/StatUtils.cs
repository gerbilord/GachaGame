public static class StatUtils
{
        
    public static bool IsSpecialStat(Stat stat)
    {
        return stat == Stat.Special1 || stat == Stat.Special2 || 
               stat == Stat.Special3 || stat == Stat.Special4;
    }

}