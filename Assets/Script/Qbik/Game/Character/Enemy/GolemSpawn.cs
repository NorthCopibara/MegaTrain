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

            if (AllData.DataZone._maxCar == AllData.NumberCar) //Разделить методы спавна для разных типов босов
            {
                obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/SuperGolem") as GameObject, transform.position, Quaternion.identity);
                f = 2;
            }
            else
            {
                obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/Golem") as GameObject, transform.position, Quaternion.identity);
                f = 1;
            }

            data.health = AllData.EnemyDataPool[f]._health;
            data.armor = AllData.EnemyDataPool[f]._armor;
            data.lvl = AllData.EnemyDataPool[f]._lvl;
            data.exp = AllData.EnemyDataPool[f]._exp;
            data.timeDeath = AllData.EnemyDataPool[f]._timeDeath;

            if (AllData.DataZone._maxCar == AllData.NumberCar)
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
            ai.Initialized(AllData.EnemyDataPool[f], tpPoint);
            ai.OnSpawn();
        }
    }
}
