using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Qbik.Static.Pool;

namespace Qbik.Loader
{
    public class Loader : MonoBehaviour
    {
        [Header("Количество мобов в пулле на старне")]
        [SerializeField] private int countEnemy;
        [Header("Имена префабов противников")]
        [SerializeField] private List<string> enemyName;

        [Space(10)]
        [SerializeField] private Button buttonStart;

        [SerializeField] private string nameScen; //Пока только для 1 сцены 

        private AsyncOperation async;

        private void Start()
        {
            buttonStart.onClick.AddListener(() => { StartGame(); });
            StartCoroutine(LoadYourAsyncScene());
        }

        private IEnumerator LoadYourAsyncScene()
        {
            yield return null;

            #region CreatePool

            ManagerPool.SetPrefab(Resources.Load<GameObject>("Models/Prefabs/Character/" + enemyName[0]));//Пока только для 1 типа противников

            ManagerPool.AddPool(PoolType.Robot); //Создание нового пула
            GameObject poolsGO = GameObject.Find("[POOLS]");

            poolsGO.AddComponent<ScenConnector>();

            ManagerPool.NewSinglSpawn(PoolType.Robot, ManagerPool.Prefab); //Это вызывается перед наполнением

            int count = 0;
            while (count < countEnemy)
            {
                ManagerPool.SinglSpawn(PoolType.Robot, ManagerPool.Prefab);
                count++;
                yield return new WaitForSeconds(0.01f);
            }

            DontDestroyOnLoad(poolsGO);
            #endregion


            async = SceneManager.LoadSceneAsync(nameScen, LoadSceneMode.Single);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                yield return null;
            }

            buttonStart.gameObject.SetActive(true);

            yield break;
        }

        public void StartGame()
        {
            async.allowSceneActivation = true;
        }
    }
}
