using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSystem : MonoBehaviour
{
    private readonly string PrefabPlayerPath = "Models/Prefabs/Character/Player";

    #region DataGame
    private PlayerData _playerData = new PlayerData(100, 2, 10, 5, 1, 25); // +A+
    private EnemyData _roboData = new EnemyData("Robot", 50, 1, 10, 200, 3f);
    private EnemyData _golemData = new EnemyData("Golem", 200, 1, 10, 800, 3f);
    private EnemyData _flyBotData = new EnemyData("FlyBot", 10, 1, 10, 200, 3f);
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

        #region Init Character Data Player
        CharacterData charData = new CharacterData(); 
        charData._health = _playerData._health;
        charData._armor = _playerData._armor;
        #endregion

        _player.GetComponent<PlayerController>().Initialized(_playerData);
        _player.GetComponent<Character>().Initialized(charData);

        FindObjectOfType<EnemySpawner>().InitializedSpawn(_roboData).InitializedSpawn(_golemData).InitializedSpawn(_flyBotData);
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
