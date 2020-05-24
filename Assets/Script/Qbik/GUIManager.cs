using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Asset.Scripts.Qbik.Static.Pool;
using Assets.Scripts.Qbik.Static.Data;

public class GUIManager : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void ReStart() 
    {
        Time.timeScale = 1;
        AllData.ClearLvl();
        ManagerPool.Dispose();
        SceneManager.LoadScene("Load");
    }

    public void Menu() 
    {
        Time.timeScale = 1;
        AllData.ClearLvl();
        ManagerPool.Dispose();
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }
}
