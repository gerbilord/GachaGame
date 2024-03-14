using System.Linq;

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

    public static Monster GetMonster(int monsterId, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        return playerBoard1.GetMonsters().Concat(playerBoard2.GetMonsters()).ToList().Find(monster=> monster.GetId() == monsterId);
    }
}
