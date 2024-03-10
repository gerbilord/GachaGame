using System.Collections.Generic;

public class PlayerBoard
{
    private Hunter _hunter;
    private List<Monster> _monsters;
    private List<Monster> _graveyard;
    
    public List<Monster> GetMonsters() => new(_monsters);

    public PlayerBoard(Hunter hunter, List<Monster> monsters)
    {
        _hunter = hunter;
        _monsters = monsters;
        _graveyard = new List<Monster>();
    }
}