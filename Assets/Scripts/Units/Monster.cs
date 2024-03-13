using System.Collections.Generic;

public class Monster
{
    private string _name;
    private int _id; // id is unique per game
    
    private int _attack;
    private int _health;
    private int _armor;
    private int _resist;
    private int _special1;
    private int _special2;
    private int _mana;
    private float _magic;
    
    private List<IPassive> _passives;
    private List<ISpell> _spells;
    
    private List<IStatus> _statuses;
    
    public Monster(int id, string name, int attack, int health, int armor, int resist, int special1, int special2, int mana, float magic, List<IPassive> passives, List<ISpell> spells)
    {
        _id = id;
        _name = name;
        _attack = attack;
        _health = health;
        _armor = armor;
        _resist = resist;
        _special1 = special1;
        _special2 = special2;
        _mana = mana;
        _magic = magic;
        _passives = passives;
        _spells = spells;
        _statuses = new List<IStatus>();
    }
    
    public Monster DeepCopy()
    {
        return new Monster(_id, _name, _attack, _health, _armor, _resist, _special1, _special2, _mana, _magic, _passives, _spells);
    }
    

    public int GetId()
    {
        return _id;
    }
    
    public int GetAttack()
    {
        return _attack;
    }

    public void TakeDamage(int damage, DamageType damageType)
    {
        _health -= damage;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetHealth()
    {
        return _health;
    }
    
    public List<PossibleAction> GetPossibleActions(PlayerBoard playerBoard1, PlayerBoard playerBoard2)
    {
        List<PossibleAction> possibleActions = new List<PossibleAction>();
        foreach (ISpell spell in _spells)
        {
            possibleActions.Add(new PossibleAction(spell.GetName(), spell.GetPossibleTargets(this, playerBoard1, playerBoard2)));
        }

        possibleActions.Add(new PossibleAction("AutoAttack", new()));
        return possibleActions;
    }
    
    public List<ISpell> GetSpells()
    {
        return _spells;
    }
}

public class DamageType
{
}