public class BoardUtils
{
    public static PlayerBoard GetMyBoard(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        if (playerBoard1.GetMonsters().Contains(monster))
        {
            return playerBoard1;
        }
        else
        {
            return playerBoard2;
        }
    }
    
    public static PlayerBoard GetEnemyBoard(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        if (playerBoard1.GetMonsters().Contains(monster))
        {
            return playerBoard2;
        }
        else
        {
            return playerBoard1;
        }
    }
}
