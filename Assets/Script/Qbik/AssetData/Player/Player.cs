using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    [Header("Основные стартовые статы")]
    [Space(2)]
    [Header("Нужно на повышение уровня:")]
    public int _upExp;
    public int _health;
    public int _armor;
    public float _speed;
    [Space(2)]
    [Header("Три параметра, которые как то влияют на атаку перса (хз как, но влияют)")]
    public float _attackRate;
    public float _nextAttackTime;
    public float _attackRange;
    [Space(2)]
    [Header("Всегда должно быть по 3!!!")]
    public List<int> _damage;
    public List<int> _damageForce;
    [Space(5)]
    [Header("Не трогать!")]
    public int _extraJump;
    public float _jumpForce;

    [Space(10)]
    [Header(" x + d * lvl")]
    [Header("Коэф. стат")]
    public int dUpExp;
    public int dHealth;
    public int dArmor;
    [Header("Всегда должно быть по 3!!!")]
    public List<int> dDamage;
}
