using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Assets.Scripts.Qbik
{
    public static class SetJson<T> where T : struct
    {
        public static void SetJsonData(string pathFile, List<T> Data)
        {
            string _path = Path.Combine(Application.persistentDataPath, pathFile);

            DataServer<T> _dataServer = new DataServer<T>();

            _dataServer.data = new List<T>();
            string json = "";

            _dataServer.data = Data; 

            json += pathFile[0] + "\n";

            if (_dataServer.data != null)
            {
                foreach (var scan in _dataServer.data)
                    json += JsonUtility.ToJson(scan) + "\n";
            }
            File.WriteAllText(_path, json);
        }
    }

    public static class GetJson<T> where T : struct 
    {
        public static List<T> GetJsonData(string pathFile, int countData)
        {
            string _path = Path.Combine(Application.persistentDataPath, pathFile);
            DataServer<T> _dataServer = new DataServer<T>();
            
            if (!File.Exists(_path))
            {
                return _dataServer.data;
            }

            string[] json = File.ReadAllLines(_path);

            for (int i = 0; i < json.Length; i++)
            {
                if (json[i][0] == pathFile[0])
                {
                    T data = new T();
                    _dataServer.data = new List<T>();
                    for (int j = i + 1; j < countData + i + 1; j++)
                        data = JsonUtility.FromJson<T>(json[j]);

                    _dataServer.data.Add(data);
                }
            }

            return _dataServer.data;
        }
    }

    public static class ChekJson 
    {
        public static bool ChekJsonData(string pathFile)
        {
            string filePath = Path.Combine(Application.persistentDataPath, pathFile);

            if (File.Exists(filePath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
[SerializeField]
public class DataServer<T>
{
    public List<T> data;
}