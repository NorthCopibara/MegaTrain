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

        FlySpawnController flySpawnController;
        MapController mapController;
        PlayerManager playerManager;

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
            InitMap();
        }

        private void InitModels()
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

            Model.InitData(dataPlayer, dataEnemy, State.Load, dataZone, endGame);
        }

        private void InitPlayer()
        {
            GameObject _player;

            #region Player Created
            _player = Resources.Load<GameObject>(PrefabPlayerPath) as GameObject;
            _player = Instantiate(_player, playerSpawn.position, Quaternion.identity);
            _player.name = "[PLAYER]";

            _player.GetComponent<PlayerController>().Initialized();

            PlayerCharacter ch = _player.GetComponent<PlayerCharacter>();

            ScenData fuk = FindObjectOfType<ScenData>();
            if (fuk != null)
            {
                List<GameObject> cam = ch.PlayerCam;

                if (cam != null)
                {
                    TPData d = fuk.ReData();
                    d.Cam_1 = cam[0];
                    d.Cam_2 = cam[1];
                    d.Cam_3 = cam[2];

                    _player.GetComponent<AnimTP>().Init(d);
                }
                else Debug.Log("Error! Absent cam obj");
            }

            Model.Game.SetStateGame(State.Roof);
            #endregion

            playerManager = new PlayerManager(_player.transform, playerSpawn, playerLastSpawn);
        }

        private void InitMap() 
        {
            flySpawnController = new FlySpawnController(zippen, dataZone._timeSpawn);
            mapController = new MapController(forvardTrain, forvardTrainLast);
        }

        private void OnDestroy()
        {
            flySpawnController.Destroy();
            mapController.Destroy();
            playerManager.Destroy();
        }
    }
}
