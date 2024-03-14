using System.Collections.Generic;
using System.Linq;

public static class GvUi
{
    public static IUiRunner ui;
    
    public static PlayerBoard playerBoard1;
    public static PlayerBoard playerBoard2;

    public static List<Monster> GetAllMonsters()
    {
        return playerBoard1.GetMonsters().Concat(playerBoard2.GetMonsters()).ToList();
    } 
}