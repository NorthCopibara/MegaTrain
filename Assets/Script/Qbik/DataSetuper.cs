using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Qbik.Static.Data;

public class DataSetuper : MonoBehaviour
{
    [SerializeField] private Player dataPlayer;
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private Transform playerLastSpawn;
    [SerializeField] private List<Enemy> dataEnemy;
    [SerializeField] private Animator zippen;
    [SerializeField] private Zone dataZone;
    [SerializeField] private GameObject forvardTrain;
    [SerializeField] private GameObject forvardTrainLast;


    private void OnEnable()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/ControlSystem/[CONTROLSYSTEM]") as GameObject);
        obj.name = "[CONTROLSYSTEM]";

        InitData();

        ControlSystem cs = obj.GetComponent<ControlSystem>();
        cs.Init();
        cs.InitMap(forvardTrain, forvardTrainLast);
    }

    private void InitData() 
    {
        #region DefSet
        DefDataPlayer dPlayer = new DefDataPlayer();
        dPlayer.dUpExp = dataPlayer.dUpExp;
        dPlayer.dHealth = dataPlayer.dHealth;
        dPlayer.dArmor = dataPlayer.dArmor;
        dPlayer.dDamage = dataPlayer.dDamage;

        DefDataEnemy dEnemy = new DefDataEnemy(); //Для роботов //Надо добавить и для голема такой
        dEnemy.dExp = dataEnemy[0].dExp;
        dEnemy.dHealth = dataEnemy[0].dHealth;
        dEnemy.dArmor = dataEnemy[0].dArmor;
        dEnemy.dDamage = dataEnemy[0].dDamage;

        Calculate.IntDefData(dPlayer, dEnemy);
        #endregion  

        ControlSystemData data = new ControlSystemData(zippen,playerSpawn ,playerLastSpawn, dataZone._timeSpawn);

        AllData.InitData(dataPlayer, dataEnemy, State.Load, data, dataZone);
    }
}
