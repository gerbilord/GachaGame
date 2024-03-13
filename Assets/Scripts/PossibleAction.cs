using System.Collections.Generic;

public class PossibleAction
{
    public string name;
    public List<Monster> possibleTargets;
    
    public PossibleAction(string name, List<Monster> possibleTargets)
    {
        this.name = name;
        this.possibleTargets = possibleTargets;
    }
}