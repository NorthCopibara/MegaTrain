using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asset.Scripts.Qbik.Static.Pool;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
   // private GameObject prefab;
    ControlSystem controlSystem;

    [Header ("Количество мобов в пулле на старне")]
    [SerializeField] private int countEnemy;
    GameObject obj;

    private void CreateGame()
    {
        //obj = Instantiate(Resources.Load<GameObject>("Models/Prefabs/ControlSystem/[CONTROLSYSTEM]") as GameObject);
        //obj.name = "[CONTROLSYSTEM]";
        //DontDestroyOnLoad(obj);

        //controlSystem = obj.GetComponent<ControlSystem>();

        //Надо наполнить данными AllData
        //Надо создать пулл объектов и поместить его в контрол систем
        //Проинициализировать спавнеры
    }

    /*
     * На выходе будет пулл объектов противников не проиничиных данными
     * Спавнеры, понимающие что им делать
     * Данные игрока
     * Проиниченный префаб игрока
     * 
     * Противники получают данные в момент спавна
     */

    private void Start()
    {
        CreateGame();
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return null;

        string name = "Game_1";

        #region CreatePool
        ManagerPool.prefab = Resources.Load<GameObject>("Models/Prefabs/Character/Robot");


        ManagerPool.AddPool(PoolType.Robot); //Создание нового пула
        GameObject poolsGO = GameObject.Find("[POOLS]");

        poolsGO.AddComponent<ScenConnector>();

        ManagerPool.NewSinglSpawn(PoolType.Robot, ManagerPool.prefab); //Это вызывается перед наполнением
        int count = 0;
        while (count < countEnemy)
        {
            ManagerPool.SinglSpawn(PoolType.Robot, ManagerPool.prefab);
            count++;
            yield return new WaitForSeconds(0.01f);
        }
        #endregion
        DontDestroyOnLoad(poolsGO);

        //Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }


        //SceneManager.MoveGameObjectToScene(poolsGO, SceneManager.GetSceneByName(name));
        //SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(name));
        //controlSystem.Init();
        async.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync(currentScene);
        
        yield break;
    }
}
