using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Qbik.Loader
{
    public class ScenConnector : MonoBehaviour
    {
        /// <summary>
        /// Не помню зачем этот костыль тут нужен
        /// Переделать
        /// </summary>

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Game_1")
            {
                GameObject obj = GameObject.Find("[SETUP]");
                gameObject.transform.parent = obj.transform;
                gameObject.transform.parent = null;
                this.enabled = false;
            }
        }
    }
}
