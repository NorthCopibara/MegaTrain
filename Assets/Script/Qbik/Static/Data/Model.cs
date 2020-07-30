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

namespace Qbik.Static.Data
{
    public static class Model
    {
        #region Player
        private static PlayerModel playerModel;
        public static PlayerModel Player => playerModel;
        #endregion

        #region Enemy
        private static EnemiesModel enemiesModel;
        public static EnemiesModel Enemies => enemiesModel;
        #endregion

        #region Game
        private static GameModel gameModel;
        public static GameModel Game => gameModel;
        #endregion

        public static void ClearLvl()
        {
            gameModel.ClearModel();
            enemiesModel.ClearEnemyActiv();
        }

        public static void InitData(Player player, List<Enemy> enemeis, State state, ControlSystemData cs, Zone zone, GameObject end)
        {
            playerModel = new PlayerModel(player);
            enemiesModel = new EnemiesModel(enemeis);
            gameModel = new GameModel(LvlState.Load, state, cs, zone, end);
        }
    }
}
