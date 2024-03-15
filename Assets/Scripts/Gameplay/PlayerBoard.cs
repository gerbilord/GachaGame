using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerBoard
{
    private Hunter _hunter;
    private List<Monster> _monsters;
    private List<Monster> _graveyard;
    
    public List<Monster> GetMonsters() => new(_monsters);
    
    public List<Monster> GetGraveyard() => new(_graveyard);
    
    public void SendMonsterToGraveyard(Monster monster)
    {
        _monsters.Remove(monster);
        _graveyard.Add(monster);
    }

    public PlayerBoard(Hunter hunter, List<Monster> monsters, List<Monster> graveyard)
    {
        _hunter = hunter;
        _monsters = monsters;
        _graveyard = graveyard;
    }

    public PlayerBoard DeepCopy()
    {
        return new PlayerBoard(_hunter.DeepCopy(), _monsters.ConvertAll(monster => monster.DeepCopy()).ToList(), _graveyard.ConvertAll(monster => monster.DeepCopy()).ToList());
    }
    
    public void SwapMonsters(Monster monster1, Monster monster2)
    {
        int index1 = _monsters.IndexOf(monster1);
        int index2 = _monsters.IndexOf(monster2);
        _monsters[index1] = monster2;
        _monsters[index2] = monster1;
    }
}