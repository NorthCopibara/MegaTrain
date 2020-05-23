using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Qbik.Static.Data;


public class ControlSystem : MonoBehaviour
{
    private readonly string PrefabPlayerPath = "Models/Prefabs/Character/Player";

    private GameObject _player;

    #region Deligate

    public delegate void UpdateController();
    public static event UpdateController update;

    public delegate void FixedUpdateController();
    public static event FixedUpdateController fixedUpdate;

    public delegate void FlySpawn();
    public static event FlySpawn flySpawn;
    #endregion

    private bool lastCar;
    private bool dSpawn;
    private float timeSpawn;
    private Animator zippen;

    #region MapLogik
    private GameObject forvardTrain;
    private GameObject forvardTrainLast;
    #endregion

    public void InitMap(GameObject forvardTrain, GameObject forvardTrainLast) 
    {
        this.forvardTrain = forvardTrain;
        this.forvardTrainLast = forvardTrainLast;
    }

    public void Init() //Вызывается из сетапа при запуске сцены
    {
        zippen = AllData.CSData.zippen;
        timeSpawn = AllData.CSData.timeSpawn;        

        #region Player Created
        _player = Resources.Load<GameObject>(PrefabPlayerPath) as GameObject;
        _player = Instantiate(_player, AllData.CSData.playerSpawn.position, Quaternion.identity);
        _player.name = "[PLAYER]";
        
        _player.GetComponent<PlayerController>().Initialized();

        Character ch = _player.GetComponent<Character>();
        //Пошла помойка... Надо как то переработать работу с камерами, все очень криво выглядит. 
        ScenData fuk = FindObjectOfType<ScenData>();
        if (fuk != null) 
        {
            List<GameObject> cam = ch.ReCam();

            TPData d = fuk.ReData();
            d.Cam_1 = cam[0];
            d.Cam_2 = cam[1];
            d.Cam_3 = cam[2];

            _player.GetComponent<AnimTP>().Init(d);
        }

        AllData.SetStateGame(State.Roof);
        #endregion
    }

    #region Update
    private void Update()
    {
        update?.Invoke();
        SpawnUpdatet();
        MapLogikUpdate();
    }

    

    private void MapLogikUpdate() 
    {
        if (!lastCar)
        {
            if (AllData.NumberCar == AllData.DataZone._maxCar - 1)
            {
                lastCar = true;
                forvardTrain.SetActive(false);
                forvardTrainLast.SetActive(true);
            }
        }
    }

    private void SpawnUpdatet() 
    {
        if (AllData.Lvl == LvlState.Sky)
        {
            SetFlySpawn();
        }
        else if (dSpawn)
        {
            StopCoroutine(TimeSpawn());
            zippen.SetTrigger("Idle");
            dSpawn = false;
        }
    }

    private IEnumerator TimeSpawn() 
    {
        yield return new WaitForSeconds(timeSpawn);
        dSpawn = false;
    }

    private void SetFlySpawn() 
    {
        if (!dSpawn)
        {
            if(AllData.NumberSpawn != 0)
                Calculate.UpRobot();

            AllData.UpSpawn();
            zippen.SetTrigger("PlaySpawn");
            dSpawn = true;
            StartCoroutine(TimeSpawn());
        }
    }

    private void FixedUpdate()
    {
        fixedUpdate?.Invoke();
        flySpawn?.Invoke();
    }
    #endregion

    public void DestroyCS()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AllData.ClearLvl(); //Чистим некоторые данные в кс
    }
}
