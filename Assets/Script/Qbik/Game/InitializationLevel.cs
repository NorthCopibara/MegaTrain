using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Data.Calculate;
using Qbik.Game.PlayerGame;
using Qbik.Game.ZoneGame.StageGame;

using JokerGho5t.MessageSystem;

namespace Qbik.Game.Data
{
    public class InitializationLevel : MonoBehaviour
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

        [Space (5)]
        [Header ("Конвас смерти игрока")]
        [SerializeField] private GameObject deathConvas;

        [Space(5)]
        [Header("Объекты сцены")]
        [SerializeField] private Animator animTrain;
        [SerializeField] private GameObject light;
        [SerializeField] private GameObject trainRoof;
        [SerializeField] private GameObject trainCar;
        [SerializeField] private GameObject trainCarLast;
        [SerializeField] private GameObject backTrain;

        private readonly string PrefabPlayerPath = "Models/Prefabs/Character/Player";

        private void OnEnable()
        {
            Message.AddListener("InitDataLevel", Init);
        }

        public void Init()
        {
            Message.RemoveListener("InitDataLevel", Init);

            InitModels();
            InitPlayer();
        }

        private void InitModels()
        {
            #region DefSet
            DefDataPlayer dPlayer = new DefDataPlayer();
            dPlayer.dUpExp = dataPlayer.dUpExp;
            dPlayer.dHealth = dataPlayer.dHealth;
            dPlayer.dArmor = dataPlayer.dArmor;
            dPlayer.dDamage = dataPlayer.dDamage;

            List<DefoultDataEnemy> wey = new List<DefoultDataEnemy>();

            DefoultDataEnemy dEnemy_1 = new DefoultDataEnemy(); //Для роботов //Надо добавить и для голема такой
            dEnemy_1.dExp = dataEnemy[0].dExp;
            dEnemy_1.dHealth = dataEnemy[0].dHealth;
            dEnemy_1.dArmor = dataEnemy[0].dArmor;
            dEnemy_1.dDamage = dataEnemy[0].dDamage;

            DefoultDataEnemy dEnemy_2 = new DefoultDataEnemy(); //Для роботов //Надо добавить и для голема такой
            dEnemy_2.dExp = dataEnemy[1].dExp;
            dEnemy_2.dHealth = dataEnemy[1].dHealth;
            dEnemy_2.dArmor = dataEnemy[1].dArmor;
            dEnemy_2.dDamage = dataEnemy[1].dDamage;

            wey.Add(dEnemy_1);
            wey.Add(dEnemy_2);

            Calculate.IntDefData(dPlayer, wey);
            #endregion

            Model.InitData(dataPlayer, dataEnemy, State.Load, dataZone, endGame);
        }

        private void InitPlayer()
        {
            GameObject _player;

            #region Player Created
            _player = Resources.Load<GameObject>(PrefabPlayerPath) as GameObject;
            _player = Instantiate(_player, playerSpawn.position, Quaternion.identity);
            _player.name = "[PLAYER]";

            _player.GetComponent<PlayerController>().Initialized(); //GAVNO

            PlayerCharacter ch = _player.GetComponent<PlayerCharacter>(); //GAVNO

            List<GameObject> cam = ch.PlayerCam;

            AnimTP animTP = null;
            if (cam != null)
            {
                TPData data = new TPData();
                data.light = light;
                data.animTrain = animTrain;
                data.trainRoof = trainRoof;
                data.trainCar = trainCar;
                data.trainCarLast = trainCarLast;
                data.backTrain = backTrain;
                data.Cam_1 = cam[0];
                data.Cam_2 = cam[1];
                data.Cam_3 = cam[2];

                animTP = new AnimTP(data);
            }
            else Debug.Log("Error! Absent cam obj");

            Model.Game.SetStateGame(State.Roof);
            #endregion

            PlayerManager playerManager = new PlayerManager(_player.transform, playerSpawn, playerLastSpawn, deathConvas);
            FlySpawnController flySpawnController = new FlySpawnController(zippen, dataZone._timeSpawn);
            MapController mapController = new MapController(forvardTrain, forvardTrainLast);

            ControlSystemMessage.SendEvent("InitControlSystem", playerManager, flySpawnController, mapController, animTP);
        }
    }
}

public struct TPData
{
    public Animator animTrain;
    public GameObject light;
    public GameObject trainRoof;
    public GameObject trainCar;
    public GameObject trainCarLast;
    public GameObject backTrain;
    public GameObject Cam_1;
    public GameObject Cam_2;
    public GameObject Cam_3;
}
