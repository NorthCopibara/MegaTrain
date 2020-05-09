using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool _paused = false;
    
    [SerializeField] private GameObject _pauseObj;

    public void SetTimeScale()
    {
        if (!_paused)
        {
            Time.timeScale = 0.0001f;
            _paused = true;
        }
        else 
        {
            Time.timeScale = 1;
            _paused = false;
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
