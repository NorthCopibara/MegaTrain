using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlayerData 
{
    public int _health;
    public int _damage;
    public float _speed;
    public int _extraJump;
    public float _jumpForce;

    public PlayerData(int _health, int _damage, float _speed, 
        int _extraJump, float _jumpForce) 
    {
        this._health = _health;
        this._damage = _damage;
        this._speed = _speed;
        this._extraJump = _extraJump;
        this._jumpForce = _jumpForce;
    }
}