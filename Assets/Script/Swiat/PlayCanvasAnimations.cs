using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCanvasAnimations : MonoBehaviour
{
    private GameObject _pauseManager;
    
    public void PlayAnimationClip(string nameOfAnimation)
    {
        GetComponent<Animation>().Play(nameOfAnimation);
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if ((nameOfAnimation == "MoveCanvasIn" && (gameObject.name == "PauseObj" || gameObject.name == "SettingsObj" || gameObject.name == "AgreeMenuObj" || gameObject.name == "AboutObj")) || (nameOfAnimation == "MoveCanvasOut" && (gameObject.name == "SettingsObj" || gameObject.name == "AgreeMenuObj" || gameObject.name == "AboutObj")))
            {
                GetComponent<Animation>()[nameOfAnimation].speed = 10000f;
            }
        }
    }
}
