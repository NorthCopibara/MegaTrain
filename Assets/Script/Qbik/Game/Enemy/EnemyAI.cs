using UnityEngine;
using Pathfinding;
using DragonBones;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts.Qbik.Static.Data;

public class EnemyAI : Controller, IPoolible
{
    private UnityEngine.Transform _enemyGFX;

    private GameObject _tphPointLeft;
    private GameObject _tphPointRight;
    private GameObject _tplPointLeft;
    private GameObject _tplPointRight;

    private float timeToAttack;
    private float timeNextAttack;
    private float timeSpawn;

    [Header("Точка атаки атаки")]
    [SerializeField] private GameObject _attackCollision;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private bool iGolem;

    [SerializeField] private GameObject _objBurByGolem;

    private Seeker _seeker;
    private Path _path;

    private int currentWaypoint = 0;
    private bool reachedEndOfPath;
    private bool cdAttack;

    private UnityEngine.Transform _target;
    private UnityEngine.Transform _playerPosition;

    private Rigidbody2D _rb;
    private float _speed;
    private float _nextWaypointDistance;
    private UnityArmatureComponent _animDragon;
    private AttackData _attackData;

    private bool moveAnimState;

    private bool oneState;
    private bool oneSet = true;

    private State myState;
    private bool iDeath;

    public bool CheckState()
    {
        if (myState != AllData.StateGame) //Если мое состояние отличается от состояния игры то начинаем думать что делать
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void InitNewState(State myState)
    {
        this.myState = myState;
    }

    private void Start()
    {
        _enemyGFX = _sprite.gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _animDragon = _sprite.GetComponent<UnityArmatureComponent>();
        _seeker = GetComponent<Seeker>();
    }

    public void Initialized(EnemyData data, List<GameObject> tpPoint) //Инит состояния
    {
        _tphPointLeft = tpPoint[0];
        _tphPointRight = tpPoint[1];
        _tplPointLeft = tpPoint[2];
        _tplPointRight = tpPoint[3];

        _playerPosition = FindObjectOfType<PlayerController>().gameObject.transform;

        moveAnimState = true; //Разрешаем бежать енеми. Добавить состояние спавна

        List<int> at = new List<int>() { data._damage };
        List<int> atF = null;

        timeSpawn = data._timeSpawn;
        timeToAttack = data._timeToAttack;
        timeNextAttack = data._timeNextAttack;
        _attackData = new AttackData(at, atF, data._targetAttack, data._type, data._tagAttack, data._timeStopAttack);
        
        if (_attackCollision != null)
            _attackCollision.GetComponent<AttackCollision>().Initialized(_attackData);

        _speed = data._speed;
        _nextWaypointDistance = data._nextWaypointDistance;

        if (iGolem)
        {
            _target = _playerPosition;
            myState = State.Car;
            
        }
        else
        {
            _target = _playerPosition;
            myState = State.Roof;
        }

    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            currentWaypoint = 0;
        }
    }

    public void UpdatePath() //Апдейтер поиска пути
    {
        if (_seeker.IsDone() && _target != null)
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
    }

    public void DethEnemy() 
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;

        _animDragon.animation.FadeIn("death", 0, 1);
        iDeath = true;
    }

    private void ControllerEnemy() 
    {
        if (iDeath)
            return;

        if (iGolem && myState != AllData.StateGame)
            return;

        if (_path == null)
            return;

        if (!cdAttack)
            Flip(_enemyGFX, _playerPosition); //Поворот спрайта

        if (currentWaypoint >= _path.vectorPath.Count) //Смотрим дистанцию до цели
        {
            reachedEndOfPath = true;

            if (!_animDragon.animation.isPlaying && cdAttack)
            {
                _animDragon.animation.Play("Idle");
            }

            if (myState == AllData.StateGame)
            {
                oneState = false;
                if (!cdAttack)
                {
                    if (_attackCollision != null)
                        StartCoroutine(TimeAttack());

                    _animDragon.animation.FadeIn("attack", 0, 1);

                    cdAttack = true;
                    StartCoroutine(EnemyAttack());
                }
            }
            else 
            {
                if (oneState == true && _target == transform) return; 

                if (iGolem) 
                {
                    _animDragon.animation.Play("Idle");
                    oneState = true;
                }
            }

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * _speed * Time.deltaTime;

        if (!cdAttack)
        {
            if (moveAnimState)
            {
                _animDragon.animation.Play("walk");
                moveAnimState = false;
            }
            Move(force, _rb);
        }
        else
        {
            Move(0, _rb);
        }

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[currentWaypoint]);

        if (distance < _nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }


    public void FixedUpdateController() //Апдейтаем поведение (данный метод можно убрать в детей и задавать различные состояния в родительском классе енеми) Надо сделать более функциональным
    {
        if (myState != AllData.StateGame) //Если мое состояние отличается от состояния игры то начинаем думать что делать
        {
            if (!iGolem)
            {
                oneSet = false;
                if (myState == State.Roof && AllData.StateGame == State.Car)
                {
                    if (_tphPointLeft != null && _tphPointRight != null)
                    {

                        if (Vector2.Distance(_tphPointLeft.transform.position, transform.position) < Vector2.Distance(_tphPointRight.transform.position, transform.position))
                            _target = _tphPointLeft.transform;
                        else
                            _target = _tphPointRight.transform;
                    }
                    else
                    {
                        _target = transform;
                    }
                }

                if (myState == State.Car && AllData.StateGame == State.Roof)
                {
                    if (_tplPointLeft != null && _tplPointRight != null)
                    {

                        if (Vector2.Distance(_tplPointLeft.transform.position, transform.position) < Vector2.Distance(_tplPointRight.transform.position, transform.position))
                            _target = _tplPointLeft.transform;
                        else
                            _target = _tplPointRight.transform;
                    }
                    else
                    {
                        _target = transform;
                    }
                }
            }
            else 
            {
                if (!oneSet)
                {
                    oneSet = true;
                    if (_objBurByGolem != null)
                         _objBurByGolem.SetActive(false);
                    _target = transform;
                }
            }
        }
        else
        {
            if (!iGolem)
            {
                if (!oneSet)
                {
                    oneSet = true;
                    _target = _playerPosition;
                }
            }
            else
            {
                if (oneSet)
                {
                    if(_objBurByGolem != null)
                        _objBurByGolem.SetActive(true);
                    oneSet = false;
                    _target = _playerPosition;
                }
            }
        }

        ControllerEnemy();
    }

    IEnumerator TimeAttack()
    {
        yield return new WaitForSeconds(timeToAttack); //Время пока фиксировано, тут надо задавать длительность существования коллайдера
        _attackCollision.SetActive(true);
    }

    IEnumerator EnemyAttack() 
    {
        yield return new WaitForSeconds(timeNextAttack);
        moveAnimState = true;
        cdAttack = false;
    }

    private void OnDisable()
    {
        ControlSystem.fixedUpdate -= FixedUpdateController; //На всякий случай
    }

    IEnumerator SpawnTime() 
    {
        yield return new WaitForSeconds(timeSpawn);
        ControlSystem.fixedUpdate += FixedUpdateController;
    }

    public void OnSpawn()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
        StartCoroutine(SpawnTime());
    }

    public void OnDespawn()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        iDeath = false;
        reachedEndOfPath = false;
        cdAttack = false;
        moveAnimState = false; 
        oneState = false;
        oneSet = false;
        StopAllCoroutines();
        _animDragon.animation.Play("Idle");
        CancelInvoke("UpdatePath");
        ControlSystem.fixedUpdate -= FixedUpdateController;
    }
}
