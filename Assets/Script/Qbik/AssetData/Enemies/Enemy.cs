using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Основные стартовые статы")]
    [Space(2)]
    [Header("Сколько дает експы:")]
    public int _exp;
    public int _health;
    public int _armor;
    public float _speed; 
    public int _damage;
    [Header("Время до срабатывания коллайдера:")]
    public float _timeToAttack;
    [Header("Время выключения коолайдера:")]
    public float _timeStopAttack;
    [Header("Время до следующей атаки:")]
    public float _timeNextAttack;
    [Header("Задержка во премя спавна:")]
    public float _timeSpawn;
    [Header("Задержка перед уничтожением объекта:")]
    public float _timeDeath;

    [Space(5)]
    [Header("Не трогать!")]
    public float _nextWaypointDistance;
    public LayerMask _targetAttack;
    public string _tagAttack;
    public TypeAttack _type;

    [Space(10)]
    [Header(" x + d * lvl")]
    [Header("Коэф. стат")]
    public int dExp;
    public int dHealth;
    public int dArmor;
    public int dDamage;
}
