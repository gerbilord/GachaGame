using System.Collections.Generic;

public class PlayerAction
{
    public int monsterId;
    public List<int> targetIds;
    
    public ISpell spell;
    
    public PlayerAction(int monsterId, List<int> targetIds, ISpell spell)
    {
        this.monsterId = monsterId;
        this.targetIds = targetIds;
        this.spell = spell;
    }
    
    public PlayerAction DeepCopy()
    {
        return new PlayerAction(monsterId, new List<int>(targetIds), spell);
    }
}