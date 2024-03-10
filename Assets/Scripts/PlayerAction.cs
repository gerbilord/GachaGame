public class PlayerAction
{
    public int monsterId;
    public int targetId;
    
    public ActionEnum actionEnum;
    
    public PlayerAction(int monsterId, int targetId, ActionEnum actionEnum)
    {
        this.monsterId = monsterId;
        this.targetId = targetId;
        this.actionEnum = actionEnum;
    }
}

public enum ActionEnum
{
    Attack,
    Spell,
}