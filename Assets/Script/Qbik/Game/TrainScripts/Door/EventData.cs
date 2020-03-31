using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData : MonoBehaviour
{
    private GameObject _player;
    private GameObject _train;

    private EnemySpawner[] spawner;

    int _tpDistance;

    private void Start()
    {
        spawner = FindObjectsOfType<EnemySpawner>();
    }

    public void SetButton(GameObject player, GameObject train, int tpDistance)
    {
        _tpDistance = tpDistance;
        _player = player;
        _train = train;
    }
    public void ClickButton()
    {
        _player.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + _tpDistance * 2, 0);

        if (_tpDistance > 0)
            _train.SetActive(false);
        else
            _train.SetActive(true);

        _train = null;
        _player = null;
    }
}
