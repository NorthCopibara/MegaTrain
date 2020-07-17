using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;

namespace Qbik.Game.EnemyGame.Spawn
{
    public class GolemSpawn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> tpPoint;
        [SerializeField] private GameObject but;

        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            GameObject obj;
            CharacterData data = new CharacterData();
            int f = 0;

            if (Model.Game.DataZone._maxCar == Model.Game.NumberCar) //Разделить методы спавна для разных типов босов
            {
                obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/SuperGolem") as GameObject, transform.position, Quaternion.identity);
                f = 2;
            }
            else
            {
                obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/Golem") as GameObject, transform.position, Quaternion.identity);
                f = 1;
            }

            data.health = Model.Enemies.EnemyDataPool[f]._health;
            data.armor = Model.Enemies.EnemyDataPool[f]._armor;
            data.lvl = Model.Enemies.EnemyDataPool[f]._lvl;
            data.exp = Model.Enemies.EnemyDataPool[f]._exp;
            data.timeDeath = Model.Enemies.EnemyDataPool[f]._timeDeath;

            if (Model.Game.DataZone._maxCar == Model.Game.NumberCar)
            {
                SuperGolem ch = obj.GetComponentInChildren<SuperGolem>();
                ch.Initialized(data);
            }
            else
            {
                Golem ch = obj.GetComponentInChildren<Golem>();
                ch.Initialized(data);
                ch.InitGolem(but);
            }

            EnemyAI ai = obj.GetComponentInChildren<EnemyAI>();
            ai.Initialized(Model.Enemies.EnemyDataPool[f], tpPoint);
            ai.OnSpawn();
        }
    }
}
