using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Static.Data.Calculate
{
    public static class Calculate 
    {
        private static int lvlRobot = 1;
        private static int lvlGolem = 1;
        private static DefDataPlayer _dDataPlayer;
        private static List<DefDataEnemy> _dDataEnemy;

        public static void IntDefData(DefDataPlayer dDataPlayer, List<DefDataEnemy> dDataEnemy)
        {
            _dDataPlayer = dDataPlayer;
            _dDataEnemy = dDataEnemy;
        }        

        public static void UpPlayer() 
        {
            PlayerData data = new PlayerData();
            data._lvl = Model.Player.PlayerData._lvl + 1;
            data._upExp = Model.Player.PlayerData._upExp + _dDataPlayer.dUpExp;
            data._exp = Model.Player.PlayerData._exp;
            data._health = Model.Player.PlayerData._health + _dDataPlayer.dHealth;
            data._armor = Model.Player.PlayerData._armor; //+ _dDataPlayer.dArmor * data._lvl;
            data._damage = new List<int>();
            data._damage.Add(Model.Player.PlayerData._damage[0] + _dDataPlayer.dDamage[0]);
            data._damage.Add(Model.Player.PlayerData._damage[1] + _dDataPlayer.dDamage[1]);
            data._damage.Add(Model.Player.PlayerData._damage[2] + _dDataPlayer.dDamage[2]);
            data._damageForce = Model.Player.PlayerData._damageForce;
            data._attackRate = Model.Player.PlayerData._attackRate;
            data._speed = Model.Player.PlayerData._speed;
            data._attackRange = Model.Player.PlayerData._attackRange;
            data._nextAttackTime = Model.Player.PlayerData._nextAttackTime;
            data._extraJump = Model.Player.PlayerData._extraJump;
            data._jumpForce = Model.Player.PlayerData._jumpForce;

            Model.Player.SetPlayer(data);
        }

        public static void InitLvlRobot(int lvl) 
        {
            lvlRobot = 1;
            Model.Enemies.NewEnemy();
            for (int i = 0; i < lvl; i++) 
            {
                UpRobot();
            }
            UpGolem();
        }

        public static void ClearData() 
        {
            lvlRobot = 1;
            lvlGolem = 1;
        }

        public static void UpGolem()
        {
            lvlGolem++;

            EnemyData data = new EnemyData();

            int fuk = 1;

            data._lvl = lvlGolem;
            data._exp = Model.Enemies.EnemyDataPool[fuk]._exp + _dDataEnemy[fuk].dExp * lvlGolem;
            data._health = Model.Enemies.EnemyDataPool[fuk]._health + lvlGolem * _dDataEnemy[fuk].dHealth;
            data._armor = Model.Enemies.EnemyDataPool[fuk]._armor + _dDataEnemy[fuk].dArmor * lvlGolem;
            data._damage = Model.Enemies.EnemyDataPool[fuk]._damage + lvlGolem * _dDataEnemy[fuk].dDamage;

            data._timeDeath = Model.Enemies.EnemyDataPool[fuk]._timeDeath;
            data._timeNextAttack = Model.Enemies.EnemyDataPool[fuk]._timeNextAttack;
            data._timeSpawn = Model.Enemies.EnemyDataPool[fuk]._timeSpawn;
            data._timeStopAttack = Model.Enemies.EnemyDataPool[fuk]._timeStopAttack;
            data._timeToAttack = Model.Enemies.EnemyDataPool[fuk]._timeToAttack;
            data._speed = Model.Enemies.EnemyDataPool[fuk]._speed;
            data._nextWaypointDistance = Model.Enemies.EnemyDataPool[fuk]._nextWaypointDistance;
            data._attackRate = Model.Enemies.EnemyDataPool[fuk]._attackRate;
            data._targetAttack = Model.Enemies.EnemyDataPool[fuk]._targetAttack;
            data._tagAttack = Model.Enemies.EnemyDataPool[fuk]._tagAttack;
            data._type = Model.Enemies.EnemyDataPool[fuk]._type;

            Model.Enemies.SetGolem(data);
        }

        public static void UpRobot() 
        {
            lvlRobot++;

            EnemyData data = new EnemyData();

            data._lvl = lvlRobot;
            data._exp = Model.Enemies.EnemyDataPool[0]._exp + _dDataEnemy[0].dExp;
            data._health = Model.Enemies.EnemyDataPool[0]._health + lvlRobot * _dDataEnemy[0].dHealth;
            data._armor = Model.Enemies.EnemyDataPool[0]._armor; //+ _dDataEnemy[0].dArmor * lvlRobot;
            data._damage = Model.Enemies.EnemyDataPool[0]._damage + lvlRobot * _dDataEnemy[0].dDamage;
            
            data._timeDeath = Model.Enemies.EnemyDataPool[0]._timeDeath;
            data._timeNextAttack = Model.Enemies.EnemyDataPool[0]._timeNextAttack;
            data._timeSpawn = Model.Enemies.EnemyDataPool[0]._timeSpawn;
            data._timeStopAttack = Model.Enemies.EnemyDataPool[0]._timeStopAttack;
            data._timeToAttack = Model.Enemies.EnemyDataPool[0]._timeToAttack;
            data._speed = Model.Enemies.EnemyDataPool[0]._speed;
            data._nextWaypointDistance = Model.Enemies.EnemyDataPool[0]._nextWaypointDistance;
            data._attackRate = Model.Enemies.EnemyDataPool[0]._attackRate;
            data._targetAttack = Model.Enemies.EnemyDataPool[0]._targetAttack;
            data._tagAttack = Model.Enemies.EnemyDataPool[0]._tagAttack;
            data._type = Model.Enemies.EnemyDataPool[0]._type;

            Model.Enemies.SetRobot(data);
        }
    }
}
