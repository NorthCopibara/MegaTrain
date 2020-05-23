using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Zone", menuName = "Zone")]
public class Zone : ScriptableObject
{
    public float _timeSpawn;
    public int _maxCar;
}
