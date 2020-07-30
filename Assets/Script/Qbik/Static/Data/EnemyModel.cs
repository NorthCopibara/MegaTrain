using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Static.Data
{
    public class EnemiesModel
    {
        private List<Enemy> defoultEnemies;
        private List<GameObject> poolActivEnemy;
        private List<EnemyData> enemyDataPool;
        public List<GameObject> PoolActivEnemy => poolActivEnemy;

        public EnemiesModel(List<Enemy> enemeis) 
        {
            defoultEnemies = enemeis;
            poolActivEnemy = new List<GameObject>();
            NewEnemy();
        }
        
        public void AddEnemyActiv(GameObject enemy)
        {
            poolActivEnemy.Add(enemy);
        }

        public void ClearEnemyActiv()
        {
            poolActivEnemy.Clear();
        }

        public List<EnemyData> EnemyDataPool => enemyDataPool;

        public void SetRobot(EnemyData data)
        {
            enemyDataPool[0] = data;
        }

        public void SetGolem(EnemyData data)
        {
            enemyDataPool[1] = data;
        }

        public void NewEnemy()
        {
            enemyDataPool = new List<EnemyData>();

            #region Robot
            EnemyData data_1 = new EnemyData();
            data_1._exp = defoultEnemies[0]._exp;
            data_1._lvl = 1;
            data_1._health = defoultEnemies[0]._health;
            data_1._armor = defoultEnemies[0]._armor;
            data_1._speed = defoultEnemies[0]._speed;
            data_1._nextWaypointDistance = defoultEnemies[0]._nextWaypointDistance;
            data_1._damage = defoultEnemies[0]._damage;
            data_1._targetAttack = defoultEnemies[0]._targetAttack;
            data_1._tagAttack = defoultEnemies[0]._tagAttack;
            data_1._type = defoultEnemies[0]._type;
            data_1._timeNextAttack = defoultEnemies[0]._timeNextAttack;
            data_1._timeSpawn = defoultEnemies[0]._timeSpawn;
            data_1._timeDeath = defoultEnemies[0]._timeDeath;
            data_1._timeStopAttack = defoultEnemies[0]._timeStopAttack;
            data_1._timeToAttack = defoultEnemies[0]._timeToAttack;

            enemyDataPool.Add(data_1);
            #endregion

            #region Golem
            EnemyData data_2 = new EnemyData();
            data_2._lvl = 1;
            data_2._exp = defoultEnemies[1]._exp;
            data_2._health = defoultEnemies[1]._health;
            data_2._armor = defoultEnemies[1]._armor;
            data_2._speed = defoultEnemies[1]._speed;
            data_2._nextWaypointDistance = defoultEnemies[1]._nextWaypointDistance;
            data_2._damage = defoultEnemies[1]._damage;
            data_2._targetAttack = defoultEnemies[1]._targetAttack;
            data_2._tagAttack = defoultEnemies[1]._tagAttack;
            data_2._type = defoultEnemies[1]._type;
            data_2._timeNextAttack = defoultEnemies[1]._timeNextAttack;
            data_2._timeSpawn = defoultEnemies[1]._timeSpawn;
            data_2._timeDeath = defoultEnemies[1]._timeDeath;
            data_2._timeStopAttack = defoultEnemies[1]._timeStopAttack;
            data_2._timeToAttack = defoultEnemies[1]._timeToAttack;
            enemyDataPool.Add(data_2);
            #endregion

            #region SuperGolem
            EnemyData data_3 = new EnemyData();
            data_3._lvl = 1;
            data_3._exp = defoultEnemies[2]._exp;
            data_3._health = defoultEnemies[2]._health;
            data_3._armor = defoultEnemies[2]._armor;
            data_3._speed = defoultEnemies[2]._speed;
            data_3._nextWaypointDistance = defoultEnemies[2]._nextWaypointDistance;
            data_3._damage = defoultEnemies[2]._damage;
            data_3._targetAttack = defoultEnemies[2]._targetAttack;
            data_3._tagAttack = defoultEnemies[2]._tagAttack;
            data_3._type = defoultEnemies[2]._type;
            data_3._timeNextAttack = defoultEnemies[2]._timeNextAttack;
            data_3._timeSpawn = defoultEnemies[2]._timeSpawn;
            data_3._timeDeath = defoultEnemies[2]._timeDeath;
            data_3._timeStopAttack = defoultEnemies[2]._timeStopAttack;
            data_3._timeToAttack = defoultEnemies[2]._timeToAttack;
            enemyDataPool.Add(data_3);
            #endregion
        }
    }
}
