using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerData 
{
    public int _health;
    public int _armor;
    public int _damage;
    public float _speed;
    public int _extraJump;
    public float _jumpForce;

    public PlayerData(int _health, int _armor, int _damage, float _speed, 
        int _extraJump, float _jumpForce) 
    {
        this._health = _health;
        this._armor = _armor;
        this._damage = _damage;
        this._speed = _speed;
        this._extraJump = _extraJump;
        this._jumpForce = _jumpForce;
    }
}

public struct EnemyData
{
    public string _name;
    public int _health;
    public int _armor;
    public int _damage;
    public float _speed;
    public float _nextWaypointDistance;

    public EnemyData(string _name, int _health, int _armor, int _damage, float _speed, float _nextWaypointDistance)
    {
        this._name = _name;
        this._health = _health;
        this._armor = _armor;
        this._damage = _damage;
        this._speed = _speed;
        this._nextWaypointDistance = _nextWaypointDistance;
    }
}