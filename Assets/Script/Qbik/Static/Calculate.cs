using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Qbik.Static.Data
{
    public static class Calculate 
    {
        private static int lvlRobot = 1;
        private static DefDataPlayer _dDataPlayer;
        private static DefDataEnemy _dDataEnemy;

        public static void IntDefData(DefDataPlayer dDataPlayer, DefDataEnemy dDataEnemy)
        {
            _dDataPlayer = dDataPlayer;
            _dDataEnemy = dDataEnemy;
        }        

        public static void UpPlayer() 
        {
            PlayerData data = new PlayerData();
            data._lvl = AllData.PlayerData._lvl + 1;
            data._upExp = AllData.PlayerData._upExp + _dDataPlayer.dUpExp * data._lvl;
            data._health = AllData.PlayerData._health + _dDataPlayer.dHealth * data._lvl;
            data._armor = AllData.PlayerData._armor + _dDataPlayer.dArmor * data._lvl;
            data._damage = new List<int>();
            data._damage.Add(AllData.PlayerData._damage[0] + _dDataPlayer.dDamage[0] * data._lvl);
            data._damage.Add(AllData.PlayerData._damage[1] + _dDataPlayer.dDamage[1] * data._lvl);
            data._damage.Add(AllData.PlayerData._damage[2] + _dDataPlayer.dDamage[2] * data._lvl);
            data._damageForce = AllData.PlayerData._damageForce;
            data._attackRate = AllData.PlayerData._attackRate;
            data._speed = AllData.PlayerData._speed;
            data._attackRange = AllData.PlayerData._attackRange;
            data._nextAttackTime = AllData.PlayerData._nextAttackTime;
            data._extraJump = AllData.PlayerData._extraJump;
            data._jumpForce = AllData.PlayerData._jumpForce;

            AllData.SetPlayer(data);
        }

        public static void InitLvlRobot(int lvl) 
        {
            lvlRobot = lvl;
        }

        public static void UpRobot() 
        {
            lvlRobot++;

            EnemyData data = new EnemyData();

            data._lvl = lvlRobot;
            data._exp = AllData.EnemyDataPool[0]._exp + _dDataEnemy.dExp * lvlRobot;
            data._health = AllData.EnemyDataPool[0]._health + lvlRobot * _dDataEnemy.dHealth; 
            data._armor = AllData.EnemyDataPool[0]._armor + _dDataEnemy.dArmor * lvlRobot;
            data._damage = AllData.EnemyDataPool[0]._damage + lvlRobot * _dDataEnemy.dDamage;
            
            data._timeDeath = AllData.EnemyDataPool[0]._timeDeath;
            data._timeNextAttack = AllData.EnemyDataPool[0]._timeNextAttack;
            data._timeSpawn = AllData.EnemyDataPool[0]._timeSpawn;
            data._timeStopAttack = AllData.EnemyDataPool[0]._timeStopAttack;
            data._timeToAttack = AllData.EnemyDataPool[0]._timeToAttack;
            data._speed = AllData.EnemyDataPool[0]._speed;
            data._nextWaypointDistance = AllData.EnemyDataPool[0]._nextWaypointDistance;
            data._attackRate = AllData.EnemyDataPool[0]._attackRate;
            data._targetAttack = AllData.EnemyDataPool[0]._targetAttack;
            data._tagAttack = AllData.EnemyDataPool[0]._tagAttack;
            data._type = AllData.EnemyDataPool[0]._type;

            AllData.SetRobot(data);
        }
    }
}
