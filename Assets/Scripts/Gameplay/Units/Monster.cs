using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

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

    private ISpell _autoAttack;
    private ISpell _swap;
    private List<IPassive> _passives;
    private List<ISpell> _spells;
    
    private List<IStatus> _statuses;
    
    public Monster(int id, string name, int attack, int health, int armor, int resist, int special1, int special2, int mana, float magic, DamageType autoAttackDamageType, List<IPassive> passives, List<ISpell> spells)
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
        _autoAttack = new AutoAttack(autoAttackDamageType);
        _swap = new Swap();
        _spells = spells;
        _statuses = new List<IStatus>();
    }
    
    public Monster DeepCopy()
    {
        return new Monster(_id, _name, _attack, _health, _armor, _resist, _special1, _special2, _mana, _magic, _autoAttack.GetDamageType(), _passives, _spells);
    }
    

    public int GetId()
    {
        return _id;
    }
    
    public int GetAttack()
    {
        return _attack;
    }

    public void TakeDamage(int damage, ISpell damagingSpell)
    {
        if(damage < 1)
        {
            return;
        }

        int damageToSubtract = damagingSpell.GetDamageType() == DamageType.Physical ? _armor : _resist;
        damage -= damageToSubtract;
        if (damage < 1)
        {
            damage = 1;
        }
        
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

        possibleActions.Add(new PossibleAction(_autoAttack.GetName(), new()));
        possibleActions.Add(new PossibleAction(_swap.GetName(), BoardUtils.GetMyBoard(this, playerBoard1, playerBoard2).GetMonsters().Where(aMonster=> aMonster != this).ToList()));
        
        foreach (ISpell spell in _spells)
        {
            possibleActions.Add(new PossibleAction(spell.GetName(), spell.GetPossibleTargets(this, playerBoard1, playerBoard2)));
        }

        return possibleActions;
    }
    
    public List<ISpell> GetSpells()
    {
        return _spells;
    }
    
    public ISpell GetAutoAttack()
    {
        return _autoAttack;
    }
    
    public ISpell GetSwap()
    {
        return _swap;
    }
}

public enum DamageType
{
    Physical,
    Magical,
}