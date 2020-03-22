using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    private readonly string PrefabPlayerPath = "Models/Prefabs/Character/Player";

    #region DataGame
    private PlayerData _playerData = new PlayerData(100, 10, 5, 1, 1000); // +A+
    #endregion

    #region Deligate
    public delegate void UpdateController();
    public static event UpdateController update;

    public delegate void FixedUpdateController();
    public static event FixedUpdateController fixedUpdate;
    #endregion

    private void Start()
    {
        GameObject _player = Resources.Load<GameObject>(PrefabPlayerPath) as GameObject;
        _player = Instantiate(_player);
        _player.GetComponent<PlayerController>().Initialized(_playerData);
    }

    #region Update
    private void Update()
    {
        if (update != null)
            update?.Invoke();
    }

    private void FixedUpdate()
    {
        if (fixedUpdate != null)
            fixedUpdate?.Invoke();
    }
    #endregion
}
