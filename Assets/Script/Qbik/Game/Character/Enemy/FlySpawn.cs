using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Pool;
using Qbik.Static.Data;

namespace Qbik.Game.EnemyGame.Spawn
{
    public class FlySpawn : MonoBehaviour
    {
        private Transform spawnPosition;
        [SerializeField] private List<GameObject> tpPoint;

        private void Start()
        {
            spawnPosition = FindObjectOfType<ControlSystem>().transform;
        }

        public void SpawnRobor()
        {
            StartCoroutine(Spawn(5));
        }


        IEnumerator Spawn(int countEnemy)
        {
            int count = 0;
            while (count < countEnemy)
            {
                if (AllData.Lvl != LvlState.Sky)
                {
                    StopAllCoroutines();
                    break;
                }

                for (int i = 0; i < 2; i++)
                {
                    float dS = Random.Range(-3, 3);
                    Vector2 pos = new Vector2(spawnPosition.position.x + dS, spawnPosition.position.y + dS);
                    GameObject obj = ManagerPool.Spawn(PoolType.Robot, ManagerPool.Prefab, pos, spawnPosition.rotation);
                    AllData.AddEnemyActiv(obj);//Добавляем в пулл активного енеми
                    CharacterData data = new CharacterData();
                    data.health = AllData.EnemyDataPool[0]._health;
                    data.armor = AllData.EnemyDataPool[0]._armor;
                    data.lvl = AllData.EnemyDataPool[0]._lvl;
                    data.exp = AllData.EnemyDataPool[0]._exp;
                    data.timeDeath = AllData.EnemyDataPool[0]._timeDeath;
                    obj.GetComponentInChildren<Character>().Initialized(data);
                    obj.GetComponentInChildren<EnemyAI>().Initialized(AllData.EnemyDataPool[0], tpPoint);
                }
                count++;
                yield return new WaitForSeconds(0.5f);
            }


        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
