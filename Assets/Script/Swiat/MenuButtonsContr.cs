using UnityEngine;
using UnityEngine.UI;

public class MenuButtonsContr : MonoBehaviour
{
    [SerializeField] private Slider sliderMusicMenu;
    [SerializeField] private GameObject soundContr;
    private float _volumeSound;
    
    private void Start()
    {
        LoadSettings();
        AudioListener.volume = _volumeSound;
        sliderMusicMenu.value = _volumeSound;
        DontDestroyOnLoad(soundContr);
    }
    
    private void Update()
    {
        SetSoundVolume(sliderMusicMenu.value);
        AudioListener.volume = _volumeSound;
    }
    
    public void StartGame()
    {
        Application.LoadLevel("exampleScene");
    }
    
    private void SetSoundVolume(float volume)
    {
        _volumeSound = volume;
        SaveSettings();
    }
    
    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("musicLevel", _volumeSound);
    }

    private void LoadSettings()
    {
        _volumeSound = PlayerPrefs.GetFloat("musicLevel", 1f);
    }
}
