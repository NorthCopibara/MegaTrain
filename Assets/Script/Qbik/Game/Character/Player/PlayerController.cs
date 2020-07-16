using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Qbik.Static.Controller;
using Qbik.Static.Log;
using Qbik.Static.Data;


namespace Qbik.Game.PlayerGame
{
    public class PlayerController : Controller, ICam, IPlayer
    {
        [Header("Спрайт для поворота")]
        [SerializeField] private Transform _enemyGFX;
        [Header("Коллайдеры атаки")]
        [SerializeField] private Transform _attackPoint;

        [Header("Смена камер при переходе между уровнями")]
        [SerializeField] private List<GameObject> _cam;

        [SerializeField] private int _distSlash;

        private Character _charact;
        private Rigidbody2D _rb;
        private Animator _animate;
        private float _speed;

        AttackData _attackData;
        private float _attackRate;
        private float _attackRange;
        private float _nextAttackTime;
        private bool _isAttack = false;
        private bool _isCombo_1 = false;
        private bool _isCombo_2 = false;

        public void Init()
        {
            _speed = AllData.PlayerData._speed;

            #region InitAttack 
            List<int> _damage = AllData.PlayerData._damage;
            List<int> _damageForce = AllData.PlayerData._damageForce;
            _attackRange = AllData.PlayerData._attackRange;
            _attackRate = AllData.PlayerData._attackRate;
            _nextAttackTime = AllData.PlayerData._nextAttackTime;
            _attackData = new AttackData(_damage, _damageForce, LayerMask.GetMask("Enemy"), TypeAttack.Fiz, "Enemy", 0);
            #endregion

            CharacterData charData = new CharacterData();
            charData.health = AllData.PlayerData._health;
            charData.armor = AllData.PlayerData._armor;
            charData.lvl = AllData.PlayerData._lvl;
            _charact.Initialized(charData);
        }

        public void Initialized()
        {
            _charact = GetComponent<Character>();
            AllData.SetPlayerInterface(GetComponent<IPlayer>()); //Выдаем статику интерфес взаимодействия с игроком
            Init();

            _rb = GetComponent<Rigidbody2D>();
            _animate = GetComponent<Animator>();

            //Подписка на апдейт
            ControlSystem.fixedUpdate += FixedUpdateController;
            ControlSystem.update += UpdateController;
        }

        public void UpdateController()
        {
            AttackElement();
            SlashElement();
        }

        public void FixedUpdateController()
        {
            if (AllData.StateGame != State.Load)
            {
                MoveElement();
                Flip(_enemyGFX.transform, _rb);

            }
        }

        private bool stopSlash;

        private void SlashElement()
        {
            if (SInput.InputSlash() && !stopSlash)
            {
                int flip = 0;
                if (_enemyGFX.transform.localScale.x > 0)
                {
                    flip = _distSlash;
                }
                else
                {
                    flip = -_distSlash;
                }
                gameObject.tag = "SLASH";
                Slash(new Vector2(flip, 0), _rb);
                stopSlash = true;
                StartCoroutine(GSlash());
            }
        }

        private IEnumerator GSlash()
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.tag = "Player";
            stopSlash = false;
        }

        private void AttackElement()
        {
            if (Time.time >= _nextAttackTime)
            {
                _isAttack = false;
                _isCombo_1 = false;
                _isCombo_2 = false;
                switch (SInput.InputAttack())
                {
                    case 1:
                        Attack(_attackRange, _attackPoint, _attackData, 1);
                        _animate.SetTrigger("AttackTrigger");
                        _nextAttackTime = Time.time + 1f / _attackRate;
                        _isAttack = true;
                        _isCombo_1 = false;
                        _isCombo_2 = false;
                        Log.MyDebugLog("PlayerController", "Attack", "IAttack");
                        break;
                }
            }
            else if (!_isCombo_1)
            {
                switch (SInput.InputAttack())
                {
                    case 1:
                        Attack(_attackRange, _attackPoint, _attackData, 2);
                        _animate.SetTrigger("ComboTrigger_1");
                        _nextAttackTime = 0;
                        _nextAttackTime = Time.time + 1f / _attackRate;
                        _isCombo_1 = true;
                        Log.MyDebugLog("PlayerController", "Attack", "ICombo");
                        break;
                }
            }
            else if (!_isCombo_2 && _isCombo_1)
            {
                switch (SInput.InputAttack())
                {
                    case 1:
                        Attack(_attackRange, _attackPoint, _attackData, 3);
                        _animate.SetTrigger("ComboTrigger_2");
                        _nextAttackTime = 0;
                        _nextAttackTime = Time.time + 1f / _attackRate;
                        _isCombo_2 = true;
                        Log.MyDebugLog("PlayerController", "Attack", "ICombo_2");
                        break;
                }
            }
        }

        public void DeathPlayer()
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;

            _animate.SetTrigger("death");
        }

        private void MoveElement()
        {
            float move = 0;
            if (!_isAttack)
                move = SInput.InputControll() * _speed;
            else
                move = 0;

            Move(move, _rb);

            if (move != 0)
            {
                _animate.SetBool("IDLE", false);
                _animate.SetBool("MOVE", true);
            }
            else
            {
                _animate.SetBool("MOVE", false);
                _animate.SetBool("IDLE", true);
            }
        }

        public void SwapCam(int i, int j)
        {
            _cam[i].SetActive(false);
            _cam[j].SetActive(true);
        }

        private void OnDisable()
        {
            ControlSystem.fixedUpdate -= FixedUpdateController;
            ControlSystem.update -= UpdateController;
        }
    }
}
