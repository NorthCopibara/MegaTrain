using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Data.Calculate;

namespace Qbik.Game.Data
{
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
        [SerializeField] private GameObject endGame;


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

            List<DefDataEnemy> wey = new List<DefDataEnemy>();

            DefDataEnemy dEnemy_1 = new DefDataEnemy(); //Для роботов //Надо добавить и для голема такой
            dEnemy_1.dExp = dataEnemy[0].dExp;
            dEnemy_1.dHealth = dataEnemy[0].dHealth;
            dEnemy_1.dArmor = dataEnemy[0].dArmor;
            dEnemy_1.dDamage = dataEnemy[0].dDamage;

            DefDataEnemy dEnemy_2 = new DefDataEnemy(); //Для роботов //Надо добавить и для голема такой
            dEnemy_2.dExp = dataEnemy[1].dExp;
            dEnemy_2.dHealth = dataEnemy[1].dHealth;
            dEnemy_2.dArmor = dataEnemy[1].dArmor;
            dEnemy_2.dDamage = dataEnemy[1].dDamage;

            wey.Add(dEnemy_1);
            wey.Add(dEnemy_2);

            Calculate.IntDefData(dPlayer, wey);
            #endregion

            ControlSystemData data = new ControlSystemData(zippen, playerSpawn, playerLastSpawn, dataZone._timeSpawn);

            AllData.InitData(dataPlayer, dataEnemy, State.Load, data, dataZone, endGame);
        }
    }
}
