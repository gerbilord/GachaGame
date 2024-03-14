public class Hunter
{ 
    IHunterPassive _hunterPassive;
    
    public Hunter(IHunterPassive hunterPassive)
    {
        _hunterPassive = hunterPassive;
    }
    
    public Hunter DeepCopy()
    {
        return new Hunter(_hunterPassive);
    }
}