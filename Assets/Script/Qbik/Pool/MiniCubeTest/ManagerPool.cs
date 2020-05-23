using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.Qbik.Static.Pool
{
    public static class ManagerPool
    {
        public static GameObject prefab;
        private static Dictionary<int, Pool> _pools = new Dictionary<int, Pool>();

        public static void NewSinglSpawn(PoolType id, GameObject prefab) 
        {
            _pools[(int)id].NewSinglSpawn(prefab);
        }

        public static GameObject SinglSpawn(PoolType id, GameObject prefab) 
        {
            return _pools[(int)id].SinglSpawn(prefab);
        }

        public static Pool AddPool(PoolType id, bool reparent = true) //size - какой по размеру должно заниматься место, для предопределения расширения листа //// reparant - используется для возвращенрия по надобности в какй то сигмент на сцене
        {
            Pool pool;

            if (!_pools.TryGetValue((int)id, out pool)) //проверка на созданный пулл
            {
                pool = new Pool();

                _pools.Add((int)id, pool);

                if (reparent)
                {
                    GameObject poolsGO = GameObject.Find("[POOLS]") ?? new GameObject("[POOLS]");
                    GameObject poolGO = new GameObject("Pool:" + id);
                    poolGO.transform.SetParent(poolsGO.transform);
                    pool.SetParant(poolGO.transform);
                }
            }
            return pool;
        }

        public static GameObject Spawn(PoolType id, GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parant = null) //Что создать
        {
            return _pools[(int)id].Spawn(prefab, position, rotation, parant); //Находим нужный пул и приводим id к целочисленному значению и передаем ему команду спавн
        }

        public static T Spawn<T>(PoolType id, GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parant = null) //Используется для других типов (не го)
        {
            var val = _pools[(int)id].Spawn(prefab, position, rotation, parant);
            return val.GetComponent<T>();
        }

        public static void DeSpawn(PoolType id, GameObject go)//Вызов деспавна у пула и объект возвращается обратоно в пулл
        {
            _pools[(int)id].Despawn(go);
        }

        public static void Dispose() //Чистка всех пулов
        {
            foreach (var poolsValue in _pools.Values)
                poolsValue.Dispose();

            _pools.Clear();
        }
    }
}
