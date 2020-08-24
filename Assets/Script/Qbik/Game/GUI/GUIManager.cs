using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Qbik.Static.Pool;
using Qbik.Static.Data;

namespace Qbik.Game.GameUI
{
    public class GUIManager : MonoBehaviour
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void ReStart()
        {
            Time.timeScale = 1;
            Model.ClearLvl();
            ManagerPool.Dispose();
            SceneManager.LoadScene("Load");
        }

        public void Menu()
        {
            Time.timeScale = 1;
            Model.ClearLvl();
            ManagerPool.Dispose();
            SceneManager.LoadScene("Menu");
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }
    }
}
