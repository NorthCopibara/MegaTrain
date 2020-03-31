using UnityEngine;
using Asset.Scripts.Qbik.Static.Controller;
using Asset.Scripts.Qbik.Static.Anim;
using System.Collections.Generic;

public class PlayerController: MonoBehaviour, ICam
{
    #region Deligate
    public delegate bool JumpInput();
    public static event JumpInput isFly;

    public delegate int AttackInput();
    public static event AttackInput isAttack;

    public delegate bool Ground(Transform groundChek, float checkRadius, LayerMask whatIsGround);
    public static event Ground isGround;

    public delegate float Input();
    public static event Input checInput;

    public delegate void Move(float move, float speed, Rigidbody2D rb);
    public static event Move isMove;

    public delegate void Flip(Rigidbody2D rb, Transform spritTransform);
    public static event Flip goFlip;
    #endregion

    [Header("Спрайт для поворота")]
    [SerializeField] private Transform _enemyGFX;
    [Header("Коллайдеры атаки")]
    [SerializeField] private List<GameObject> _colliderAttack;

    [SerializeField] private List<GameObject> _cam;

    [Space (5)]
    [Header("Объект чека земли")]
    [SerializeField] private Transform _groundChek;
    [Header("Радиус проверки земли")]
    [SerializeField] private float _checkRadius;
    [Header("Обозначение слоя земли")]
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rb;
    private Animator _animate;
    private int _extraJump;
    private float _jumpForce;
    private float _speed;

    private bool _isJump;
    private bool _isAttack;

    State _lastState;

    private void Start()
    {
        _rb      = GetComponent<Rigidbody2D>();
        _animate = GetComponent<Animator>();
    }

    public void Initialized(PlayerData data) 
    {
        _extraJump = data._extraJump;
        _speed = data._speed;
        _jumpForce = data._jumpForce;

        isAttack    += SInput.InputAttack;
        isFly       += SInput.InputJump;
        checInput   += SInput.InputControll;

        #region InitAttack
        AttackData _attackData = new AttackData(data._damage, "Enemy", TypeAttack.Fiz);
        _colliderAttack[0].GetComponent<AttackCollision>().Initialized(_attackData);
        #endregion

        //Подписка на апдейт
        ControlSystem.fixedUpdate += FixedUpdateController;
    }

    public void FixedUpdateController() 
    {
        #region Jump
        if (isFly != null) 
        {
            if ((bool)isFly?.Invoke()) 
            {
                Debug.Log("jump");
                isFly    -= SInput.InputJump;
                isGround += SController.IsGround;
                SController.Jump(_extraJump, _jumpForce, _rb);

                SetLastState();
                StateMachine.SetState(State.JUMP, _animate);
            }
        }
        
        if (isGround != null) 
        {
            if (!(bool)isGround?.Invoke(_groundChek, _checkRadius, _whatIsGround))
            {
                _isJump = true; //Ждем достаточной высоты для активации чека
            }
            else if (_isJump)
            {
                Debug.Log("ground");
                isGround -= SController.IsGround;
                isFly += SInput.InputJump;

                StateMachine.SetState(_lastState, _animate);
                _isJump = false;
            }
        }

        if (isFly != null && isGround != null) //костыль на баг
        {
            isGround -= SController.IsGround;
            Debug.Log("error");
        }
        #endregion

        #region Move
        if (checInput != null) //Вводные перемещения
        {
            if (checInput?.Invoke() != 0 && isMove == null)
            {
                isMove += SController.Move;
                goFlip += SController.Flip;

                StateMachine.SetState(State.MOVE, _animate);
                SetLastState();
            }
            else if(checInput?.Invoke() == 0 && isMove != null)
            {
                isMove?.Invoke(0, _speed, _rb); //Костыль на отпись, сохраняется текущее значение, изменить контроллер
                isMove -= SController.Move;
                goFlip -= SController.Flip;

                StateMachine.SetState(State.IDLE, _animate);
                SetLastState();
            }
        }

        if (isMove != null)
        {
            if (!_isAttack)
            {
                isMove?.Invoke(SInput.InputControll(), _speed, _rb);
                if (goFlip != null)
                {
                    goFlip?.Invoke(_rb, _enemyGFX);
                }
            }
            else
            {
                isMove?.Invoke(0, _speed, _rb);
            }
        }
        #endregion
        
        #region Attack
        if (isAttack != null) //костыль на фикс проверки
        {
            if (isAttack?.Invoke() == 1)
            {
                SController.AttackCollision(_colliderAttack[0], true);

                if(!_isAttack)
                    SetLastState();
                StateMachine.SetState(State.ATTACK, _animate);

                isFly -= SInput.InputJump;
                checInput -= SInput.InputControll;
                if(isMove != null)
                    isMove?.Invoke(0, _speed, _rb);


                _isAttack = true;
            }

            if (!_colliderAttack[0].active && _isAttack)
            {
                StateMachine.SetState(_lastState, _animate);
                SetLastState();
                _isAttack = false;

                isFly += SInput.InputJump;
                checInput += SInput.InputControll;
            }
        }
        #endregion

        

        if (isFly == null && checInput == null && isAttack == null) //Костыль на нулевое состояние
        {
            StateMachine.SetState(State.IDLE, _animate);
        }
    }

    private void SetLastState()
    {
        _lastState = StateMachine._state;
    }

    public void SwapCam(int i, int j) 
    {
        _cam[i].SetActive(false);
        _cam[j].SetActive(true);
    }
}
