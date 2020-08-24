using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Game;

public class MapController
{
    private bool lastCar;

    #region MapLogik
    private GameObject forvardTrain;
    private GameObject forvardTrainLast;
    #endregion

    public MapController(GameObject forvardTrain, GameObject forvardTrainLast)
    {
        this.forvardTrain = forvardTrain;
        this.forvardTrainLast = forvardTrainLast;

        ControlSystem.update += MapLogikUpdate;
    }

    private void MapLogikUpdate()
    {
        if (!lastCar)
        {
            if (Model.Game.NumberCar == Model.Game.DataZone._maxCar - 1) //Костыль, переделать на ивентах
            {
                lastCar = true;
                forvardTrain.SetActive(false);
                forvardTrainLast.SetActive(true);
            }
        }
    }

    public void Destroy() 
    {
        ControlSystem.update -= MapLogikUpdate;
    }
}
