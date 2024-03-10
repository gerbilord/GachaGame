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
    
    private IPassive _passive;
    private ISpell _spell;
    
    private List<IStatus> _statuses;
    
    public Monster(int id, string name, int attack, int health, int armor, int resist, int special1, int special2, int mana, float magic, IPassive passive, ISpell spell)
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
        _passive = passive;
        _spell = spell;
        _statuses = new List<IStatus>();
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
}

public class DamageType
{
}