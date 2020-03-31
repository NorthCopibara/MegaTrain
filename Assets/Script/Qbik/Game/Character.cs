using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITakeDamage
{
    private int _charHealth;
    private int _charArmor;

    [SerializeField] private HealthBar _healthBar;

    public void Initialized(CharacterData dataInit) 
    {
        _charHealth = dataInit._health;
        _charArmor  = dataInit._armor;
    }

    public void TakeDamage(AttackData attack)
    {
        int damage = attack._damage / _charArmor;
        _charHealth -= damage;

        ChekDeth(damage);
    }

    private void ChekDeth(int damage) 
    {
        if (_charHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if(_healthBar != null)
                _healthBar.ApllyDamage(damage);
        }
    }
}

public struct CharacterData 
{
    public int _health;
    public int _armor;
}
