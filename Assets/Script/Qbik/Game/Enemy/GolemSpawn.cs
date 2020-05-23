using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Qbik.Static.Data;

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
        //Надо добавить метод переинита голема в статики, пока просто спавним новго
        if (AllData.DataZone._maxCar == AllData.NumberCar)
        {
            obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/SuperGolem") as GameObject, transform.position, Quaternion.identity);
            f = 2;
        }
        else
        {
            obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/Character/Golem") as GameObject, transform.position, Quaternion.identity);
            f = 1;
        }

        data._health = AllData.EnemyDataPool[f]._health;
        data._armor = AllData.EnemyDataPool[f]._armor;
        data._lvl = AllData.EnemyDataPool[f]._lvl;
        data._exp = AllData.EnemyDataPool[f]._exp;

        Character ch = obj.GetComponentInChildren<Character>();
        ch.Initialized(data);
        ch.InitGolem(but);
        EnemyAI ai = obj.GetComponentInChildren<EnemyAI>();
        ai.Initialized(AllData.EnemyDataPool[f], tpPoint);
        ai.OnSpawn();
    }
}
