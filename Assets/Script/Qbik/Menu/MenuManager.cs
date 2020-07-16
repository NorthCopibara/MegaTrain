using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Qbik.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button startGame;

        private void Start()
        {
            startGame.onClick.AddListener(() => { StartGame(); });
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Load");
        }
    }
}
