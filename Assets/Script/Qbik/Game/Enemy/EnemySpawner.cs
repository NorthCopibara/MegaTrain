using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private readonly string PrefabPlayerPath = "Models/Prefabs/Character/";
    private List<EnemyData> _allData;

    public EnemySpawner InitializedSpawn(EnemyData data)
    {
        _allData = new List<EnemyData>();
        _allData.Add(data);

        GameObject _robot = Resources.Load<GameObject>(PrefabPlayerPath + data._name) as GameObject;

        CharacterData charData = new CharacterData();
        charData._health = data._health;
        charData._armor = data._armor;

        _robot = Instantiate(_robot, transform.position, Quaternion.identity);
        _robot.gameObject.transform.localScale = new Vector2(0.25f, 0.25f);
        _robot.GetComponentInChildren<EnemyAI>().Initialized(_allData[0]);
        _robot.GetComponentInChildren<Character>().Initialized(charData);

        return this;
    }
}
