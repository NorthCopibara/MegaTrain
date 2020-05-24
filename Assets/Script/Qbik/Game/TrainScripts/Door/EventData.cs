using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Qbik.Static.Data;

public class EventData : MonoBehaviour
{
    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject _effects;
    [SerializeField] private GameObject _trainSprite;

    private GameObject _player;
    private GameObject _train;

    int _tpDistance;

    private void OnEnable()
    {
        if (AllData.Lvl == LvlState.Last)
            gameObject.SetActive(false);
    }

    public void SetButton(GameObject player, GameObject train, int tpDistance)
    {
        _tpDistance = tpDistance;
        _player = player;
        _train = train;
    }
    public void ClickButton()
    {
        if (_player == null || _train == null)
            return;

        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + _tpDistance * 2, 0);

        if (_tpDistance > 0)
        {
            //_player.GetComponent<PlayerController>().SwapCam(1, 0);
            AllData.SetStateGame(State.Roof);
            _trainSprite.SetActive(true);
            _train.SetActive(false);
            _light.SetActive(true);
            _effects.SetActive(true);
        }
        else
        {
            //_player.GetComponent<PlayerController>().SwapCam(0, 1);
            AllData.SetStateGame(State.Car);
            _trainSprite.SetActive(false);
            _light.SetActive(false);
            _effects.SetActive(false);
            _train.SetActive(true);
        }

        _train = null;
        _player = null;
    }

    
}
