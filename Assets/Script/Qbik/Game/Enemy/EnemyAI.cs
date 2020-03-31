using UnityEngine;
using Pathfinding;
using DragonBones;
using System.Collections.Generic;
public class EnemyAI: MonoBehaviour
{
    Controller controller = new Controller(); ///Убрать это гавно

    private UnityEngine.Transform _enemyGFX;

    [Header("Коллайдеры атаки")]
    [SerializeField] private List<GameObject> _colliderAttack;

    private Seeker _seeker;
    private Path _path;

    private int currentWaypoint = 0;
    private bool reachedEndOfPath;
    private bool cdAttack;

    private UnityEngine.Transform _target;
    private Rigidbody2D _rb;
    private float _speed; 
    private float _nextWaypointDistance;
    private UnityArmatureComponent _animDragon;
    private void Start()
    {
        _enemyGFX = gameObject.transform;
        _rb = GetComponent<Rigidbody2D>();
        _animDragon = _enemyGFX.GetComponent<UnityArmatureComponent>();
        _seeker = GetComponent<Seeker>();
    }
    
    public void Initialized(EnemyData data) //Инит состояния
    {
        _target = FindObjectOfType<PlayerController>().gameObject.transform;

        _speed = data._speed;
        _nextWaypointDistance = data._nextWaypointDistance;

        ControlSystem.fixedUpdate += FixedUpdateController;
        InvokeRepeating("UpdatePath", 0f, .5f);
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
        if(_seeker.IsDone())
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
    }
    
    public void FixedUpdateController() //Апдейтаем поведение (данный метод можно убрать в детей и задавать различные состояния в родительском классе енеми) Надо сделать более функциональным
    {
        if (_path == null)
            return;

        controller.Flip(_rb, _enemyGFX) ; //Поворот спрайта

        if (currentWaypoint >= _path.vectorPath.Count) //Смотрим дистанцию до цели
        {
            reachedEndOfPath = true;
            controller.AttackCollision(_colliderAttack, _animDragon); //Если мы на минимальной дистанции, атакуем 
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * _speed * Time.deltaTime;

        controller.Move(force, _animDragon, _rb);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[currentWaypoint]);

        if (distance < _nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}

/// <summary>
/// GAVNOOOOOO
/// </summary>

public class Controller 
{
    bool _stopMoveAnim;

    public void AttackCollision(List<GameObject> _colliderAttack, UnityArmatureComponent _animDragon)
    {
        if (!_colliderAttack[0].active)
        {
            _colliderAttack[0].SetActive(true); //Активируем объект коллизии, который сам себя выключит

            if (_animDragon != null)
            {
                _animDragon.animation.FadeIn("attack");
                _stopMoveAnim = false;
            }
        }
    }

    public void Move(Vector2 move, UnityArmatureComponent _animDragon, Rigidbody2D _rb)
    {
        _rb.AddForce(move);

        if (!_stopMoveAnim)
        {
            _stopMoveAnim = true;
            _animDragon.animation.FadeIn("walk");
            _animDragon.animation.FadeIn("polet");
        }
    }

    public void Flip(Rigidbody2D _rb, UnityEngine.Transform _enemyGFX)
    {
        if (_rb.velocity.x >= 0.01f)
        {
            _enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        if (_rb.velocity.x <= -0.01f)
        {
            _enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
