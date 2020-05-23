using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Roof,
    Car,
    Load
}

public enum LvlState 
{
    Sky,
    Tunel,
    Last,
    Load
}

public struct ControlSystemData
{

    public float timeSpawn;
    public Animator zippen;
    public Transform playerSpawn;
    public Transform playerLastSpawn;

    public ControlSystemData(Animator zippen, Transform playerSpawn,Transform playerLastSpawn, float timeSpawn)
    {
        this.zippen = zippen;
        this.playerSpawn = playerSpawn;
        this.playerLastSpawn = playerLastSpawn;
        this.timeSpawn = timeSpawn;
    }
}

public struct EnemyData 
{
    public int _exp;
    public int _lvl;
    public int _health;
    public int _armor;
    public float _speed;
    public float _timeToAttack;
    public float _timeNextAttack;
    public float _timeStopAttack;
    public float _timeSpawn;
    public float _timeDeath;
    public float _nextWaypointDistance;
    public int _damage;
    public float _attackRate;
    public LayerMask _targetAttack;
    public string _tagAttack;
    public TypeAttack _type;
}

public struct DefDataPlayer 
{
    public int dUpExp;
    public int dHealth;
    public int dArmor;
    public List<int> dDamage;
}

public struct DefDataEnemy
{
    public int dExp;
    public int dHealth;
    public int dArmor;
    public int dDamage;
}

public struct PlayerData 
{
    public int _exp;
    public int _upExp;
    public int _lvl;
    public int _health;
    public int _armor;
    public List<int> _damage;
    public List<int> _damageForce;
    public float _attackRate;
    public float _nextAttackTime;
    public float _attackRange;
    public float _speed;
    public int _extraJump;
    public float _jumpForce;
}

namespace Assets.Scripts.Qbik.Static.Data
{
    public static class AllData
    {
        private static IPlayer iPlayer;
        private static Zone dataZone;
        private static int numberSpawn;
        private static int numberCar;
        private static PlayerData playerData;
        private static List<EnemyData> enemyDataPool;
        private static List<GameObject> poolActivEnemy = new List<GameObject>();
        private static State stateGame;
        private static LvlState lvl;
        private static ControlSystemData csData = new ControlSystemData();

        #region Player
        public static void SetPlayerInterface(IPlayer player) 
        {
            iPlayer = player;
        }

        public static PlayerData PlayerData
        {
            get
            {
                return playerData;
            }
        }

        private static void NewPlayer(Player player) 
        {
            playerData = new PlayerData();
            playerData._exp = 0;
            playerData._upExp = player._upExp;
            playerData._lvl = 1;
            playerData._health = player._health;
            playerData._armor = player._armor;
            playerData._damage = player._damage;
            playerData._damageForce = player._damageForce;
            playerData._attackRate = player._attackRate;
            playerData._speed = player._speed;
            playerData._extraJump = player._extraJump;
            playerData._jumpForce = player._jumpForce;
        }

        public static void SetPlayer(PlayerData data) 
        {
            playerData = data;
        }

        public static void AddExp(int exp) 
        {
            playerData._exp += exp;

            if (playerData._exp > playerData._upExp) 
            {
                //Надо событие инициализации игрока
                playerData._exp -= playerData._upExp;
                Calculate.UpPlayer();
                iPlayer.Init();
                AddExp(0); //Проверка на ап более одного уровня
            }

        }
        #endregion

        #region Enemy
        public static List<GameObject> PoolActivEnemy 
        {
            get 
            {
                return poolActivEnemy;
            }
        }

        public static void AddEnemyActiv(GameObject enemy) 
        {
            poolActivEnemy.Add(enemy);
        }

        public static void ClearEnemyActiv() 
        {
            poolActivEnemy.Clear();
        }

        public static List<EnemyData> EnemyDataPool
        {
            get
            {
                return enemyDataPool;
            }
        }

        private static void NewEnemy(List<Enemy> enemy)
        {
            enemyDataPool = new List<EnemyData>();

            #region Robot
            EnemyData data_1 = new EnemyData();
            data_1._exp = enemy[0]._exp; 
            data_1._lvl = 1;
            data_1._health = enemy[0]._health;
            data_1._armor = enemy[0]._armor;
            data_1._speed = enemy[0]._speed;
            data_1._nextWaypointDistance = enemy[0]._nextWaypointDistance;
            data_1._damage = enemy[0]._damage;
            data_1._targetAttack = enemy[0]._targetAttack;
            data_1._tagAttack = enemy[0]._tagAttack;
            data_1._type = enemy[0]._type;
            data_1._timeNextAttack = enemy[0]._timeNextAttack;
            data_1._timeSpawn = enemy[0]._timeSpawn;
            data_1._timeDeath = enemy[0]._timeDeath;
            data_1._timeStopAttack = enemy[0]._timeStopAttack;
            data_1._timeToAttack = enemy[0]._timeToAttack;

            enemyDataPool.Add(data_1);
            #endregion

            #region Golem
            EnemyData data_2 = new EnemyData();
            data_2._lvl = 1;
            data_2._exp = enemy[1]._exp;
            data_2._health = enemy[1]._health;
            data_2._armor = enemy[1]._armor;
            data_2._speed = enemy[1]._speed;
            data_2._nextWaypointDistance = enemy[1]._nextWaypointDistance;
            data_2._damage = enemy[1]._damage;
            data_2._targetAttack = enemy[1]._targetAttack;
            data_2._tagAttack = enemy[1]._tagAttack;
            data_2._type = enemy[1]._type;
            data_2._timeNextAttack = enemy[1]._timeNextAttack;
            data_2._timeSpawn = enemy[1]._timeSpawn;
            data_2._timeDeath = enemy[1]._timeDeath;
            data_2._timeStopAttack = enemy[1]._timeStopAttack;
            data_2._timeToAttack = enemy[1]._timeToAttack;
            enemyDataPool.Add(data_2);
            #endregion

            #region SuperGolem
            EnemyData data_3 = new EnemyData();
            data_3._lvl = 1;
            data_3._exp = enemy[2]._exp;
            data_3._health = enemy[2]._health;
            data_3._armor = enemy[2]._armor;
            data_3._speed = enemy[2]._speed;
            data_3._nextWaypointDistance = enemy[2]._nextWaypointDistance;
            data_3._damage = enemy[2]._damage;
            data_3._targetAttack = enemy[2]._targetAttack;
            data_3._tagAttack = enemy[2]._tagAttack;
            data_3._type = enemy[2]._type;
            data_3._timeNextAttack = enemy[2]._timeNextAttack;
            data_3._timeSpawn = enemy[2]._timeSpawn;
            data_3._timeDeath = enemy[2]._timeDeath;
            data_3._timeStopAttack = enemy[2]._timeStopAttack;
            data_3._timeToAttack = enemy[2]._timeToAttack;
            enemyDataPool.Add(data_3);
            #endregion
        }

        public static void SetRobot(EnemyData data)
        {
            enemyDataPool[0] = data;
        }
        #endregion

        #region Game

        public static Zone DataZone 
        {
            get { return dataZone; }
        }

        public static int NumberCar
        {
            get { return numberCar; }
        }
        
        public static int NumberSpawn
        {
            get { return numberSpawn; }
        }

        public static void UpCar() //Не плюсуем на последнем вагоне
        {
            numberCar++;
        }

        public static void ClearLvl() 
        {
            numberCar = 0;
            numberSpawn = 0;
            Calculate.InitLvlRobot(1);
        }

        public static LvlState Lvl
        {
            get
            {
                return lvl;
            }
        }

        public static ControlSystemData CSData
        {
            get
            {
                return csData;
            }
        }

        public static State StateGame
        {
            get
            {
                return stateGame;
            }
        }


        public static void SetSpawn(int spawn)
        {
            numberSpawn = spawn;
        }

        public static void UpSpawn()
        {
            numberSpawn++;
        }

        public static void SetStateGame(State state)
        {
            stateGame = state;
        }

        public static void SetStateLvl(LvlState state)
        {
            lvl = state;
        }
        #endregion

        public static void InitData(Player player, List<Enemy> enemy, State state, ControlSystemData cs, Zone zone) 
        {
            dataZone = zone;
            lvl = LvlState.Sky;
            csData = cs;
            NewPlayer(player);
            NewEnemy(enemy);
            stateGame = state;
        }
    }
}
