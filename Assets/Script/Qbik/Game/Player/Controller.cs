using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller: MonoBehaviour
{
    private bool _flip;
    protected void Attack(float attackRange, Transform attackPoint, AttackData data, int num)
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, data._targetAttack);
        
        foreach (Collider2D target in hitTargets) 
        {
            target.GetComponent<ITakeDamage>().TakeDamage(data, num);

            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb != null) 
            {
                float _forcePik = data._damageForce[num - 1];

                Vector2 force = new Vector2(_forcePik, _forcePik);

                if (target.transform.position.x < transform.position.x)
                    rb.AddForceAtPosition(-force, transform.position);
                else
                    rb.AddForceAtPosition(force, transform.position);
            }
        }
    }

    protected void Jump(int extraJump, float jumpForce, Rigidbody2D rb)
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    protected bool IsGround(Transform groundChek, float checkRadius, LayerMask whatIsGround)
    {
        bool _isGround = Physics2D.OverlapCircle(groundChek.position, checkRadius, whatIsGround);
        return _isGround;
    }

    protected void Slash(Vector2 move, Rigidbody2D _rb) 
    {
        _rb.AddForce(move);
    }

    protected void Move(Vector2 move, Rigidbody2D _rb)
    {
        _rb.AddForce(move);
    }


    protected static void Move(float move, Rigidbody2D rb)
    {
        rb.velocity = new Vector2(move, rb.velocity.y);
    }

    protected void Flip(Transform _sprite, Rigidbody2D _rb) //Переделать / попробовать убрать зависимость от  рб
    {
        if (_rb.velocity.x >= 0.1f)
        {
            if (_flip == false)
                return;

            _flip = false;
            SetFlip(_sprite);
        }
        else
        if (_rb.velocity.x <= -0.1f)
        {
            if (_flip == true)
                return;

            _flip = true;
            SetFlip(_sprite);
        }
    }
    protected void Flip(Transform _sprite, Transform _targer) //Переделать / попробовать убрать зависимость от  рб
    {
        if (_sprite.position.x == _targer.position.x)
            return;

        if (_sprite.position.x < _targer.position.x)
        {
            if (_flip == false)
                return;

            _flip = false;
            SetFlip(_sprite);
        }
        else
        {
            if (_flip == true)
                return;

            _flip = true;
            SetFlip(_sprite);
        }

    }

    private void SetFlip(Transform _sprite) 
    {
        _sprite.localScale = new Vector3(-_sprite.localScale.x, _sprite.localScale.y, _sprite.localScale.z);
    }
}

public enum TypeAttack
{
    Fiz,
    Mag
}

public struct AttackData
{
    public List<int> _damage;
    public List<int> _damageForce;
    public LayerMask _targetAttack;
    public TypeAttack _type;
    public string _tagAttack;
    public float _timeStopAttack;

    public AttackData(List<int> _damage, List<int> _damageForce, LayerMask _targetAttack, TypeAttack _type, string _tagAttack, float _timeStopAttack)
    {
        this._tagAttack = _tagAttack;
        this._damageForce = _damageForce;
        this._damage = _damage;
        this._targetAttack = _targetAttack;
        this._type = _type;
        this._timeStopAttack = _timeStopAttack;
    }
}
