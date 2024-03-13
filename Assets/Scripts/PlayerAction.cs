public class PlayerAction
{
    public int monsterId;
    public int targetId;
    
    public ISpell spell;
    
    public PlayerAction(int monsterId, int targetId, ISpell spell)
    {
        this.monsterId = monsterId;
        this.targetId = targetId;
        this.spell = spell;
    }
}