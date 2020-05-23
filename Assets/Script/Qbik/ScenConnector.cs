using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenConnector : MonoBehaviour
{
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
