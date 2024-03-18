using System.Collections.Generic;
using System.Linq;

public class Swap : ISpell
{
    public DamageType GetDamageType()
    {
        return DamageType.Physical; 
    }

    public string GetName()
    {
        return "Swap";
    }

    public List<Monster> GetPossibleTargets(Monster monster, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        return BoardUtils.GetMyBoard(monster, playerBoard1, playerBoard2).GetMonsters().Where(aMonster => aMonster != monster).ToList();
    }

    public PlayerAction Cast(PlayerAction playerAction, PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        Monster monster = BoardUtils.GetMonster(playerAction.monsterId, playerBoard1, playerBoard2);
        Monster target = BoardUtils.GetMonster(playerAction.targetIds[0], playerBoard1, playerBoard2);
        
        PlayerBoard myBoard = BoardUtils.GetMyBoard(monster, playerBoard1, playerBoard2);
        
        myBoard.SwapMonsters(monster, target);
        return playerAction;
    }
}