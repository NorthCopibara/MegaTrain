using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Sprite[] bgImages = new Sprite[2];
    public Sprite[] lvlImages = new Sprite[4];
    public GameObject mainMg;
    public GameObject[] lvlButtons = new GameObject[3];

    private int _lvlsAvailable;

    private void Start()
    {
        LoadSettings();
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeMainBg()
    {
        if (mainMg.GetComponent<Image>().sprite == bgImages[0])
        {
            mainMg.GetComponent<Image>().sprite = bgImages[1];
        }
        else if (mainMg.GetComponent<Image>().sprite == bgImages[1])
        {
            mainMg.GetComponent<Image>().sprite = bgImages[0];
        }
    }

    public void ChangeAvailableLvls(int num)
    {
        _lvlsAvailable = num;
        SaveSettings();
        SetAvailableLvls();
    }

    private void SetAvailableLvls()
    {
        switch (_lvlsAvailable)
        {
            case 1:
                lvlButtons[0].GetComponent<Image>().sprite = lvlImages[0];
                lvlButtons[1].GetComponent<Image>().sprite = lvlImages[3];
                lvlButtons[2].GetComponent<Image>().sprite = lvlImages[3];
                break;
            case 2:
                lvlButtons[0].GetComponent<Image>().sprite = lvlImages[0];
                lvlButtons[1].GetComponent<Image>().sprite = lvlImages[1];
                lvlButtons[2].GetComponent<Image>().sprite = lvlImages[3];
                break;
            case 3:
                lvlButtons[0].GetComponent<Image>().sprite = lvlImages[0];
                lvlButtons[1].GetComponent<Image>().sprite = lvlImages[1];
                lvlButtons[2].GetComponent<Image>().sprite = lvlImages[2];
                break;
            default:
                break;
        }
    }

    private void LoadSettings()
    {
        _lvlsAvailable = PlayerPrefs.GetInt("LvlsAvailable");
        SetAvailableLvls();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("LvlsAvailable", _lvlsAvailable);
    }
}
