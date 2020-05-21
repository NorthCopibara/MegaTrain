using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public bool _paused = false;
    public Sprite[] upDownImages;
    public GameObject upDown;
    
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

    public void changeUpDownImage()
    {
        if (upDown.GetComponent<Image>().sprite.name == "down")
        {
            upDown.GetComponent<Image>().sprite = upDownImages[0];
        }
        else
        {
            upDown.GetComponent<Image>().sprite = upDownImages[1];
        }
    }
}
