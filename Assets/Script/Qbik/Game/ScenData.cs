using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TPData
{
    public Animator animTrain;
    public GameObject light;
    public GameObject trainRoof;
    public GameObject trainCar;
    public GameObject backTrain;
    public GameObject Cam_1;
    public GameObject Cam_2;
    public GameObject Cam_3;
}

public class ScenData : MonoBehaviour
{
    [SerializeField] private Animator animTrain;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject trainRoof;
    [SerializeField] private GameObject trainCar;
    [SerializeField] private GameObject backTrain;

    
    public TPData ReData() 
    {
        TPData data = new TPData();
        data.light = light;
        data.animTrain = animTrain;
        data.trainRoof = trainRoof;
        data.trainCar = trainCar;
        data.backTrain = backTrain;
        data.Cam_1 = null;
        data.Cam_2 = null;
        data.Cam_3 = null;

        return data;
    }
}
