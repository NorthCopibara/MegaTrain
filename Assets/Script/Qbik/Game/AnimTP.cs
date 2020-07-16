﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Game.EnemyGame.Spawn;

namespace Qbik.Game.ZoneGame.StageGame
{
    public class AnimTP : MonoBehaviour
    {
        private Animator animTrain;
        private GameObject light;
        private GameObject trainRoof;
        private GameObject trainCar;
        private GameObject trainCarLast;
        private GameObject backTrain;
        private GameObject Cam_1;
        private GameObject Cam_2;
        private GameObject Cam_3;


        public void Init(TPData data)
        {
            light = data.light;
            animTrain = data.animTrain;
            trainRoof = data.trainRoof;
            trainCar = data.trainCar;
            trainCarLast = data.trainCarLast;
            backTrain = data.backTrain;
            Cam_1 = data.Cam_1;
            Cam_2 = data.Cam_2;
            Cam_3 = data.Cam_3;
        }

        public void FirstStep()
        {
            light.SetActive(true);
            backTrain.SetActive(true);
            trainRoof.SetActive(true);
            trainCar.SetActive(false);
            Cam_2.SetActive(false);
            Cam_1.SetActive(false);
            Cam_3.SetActive(true);
            animTrain.SetTrigger("crash");

            StartCoroutine(dTime());
        }

        public void LastStep()
        {
            AllData.SetStateGame(State.Load);
            AllData.SetStateLvl(LvlState.Last);
            trainCar.SetActive(false);
            trainCarLast.SetActive(true);
            StartCoroutine(lTime());
        }

        private IEnumerator lTime()
        {
            yield return new WaitForSeconds(2f);
            AllData.SetStateGame(State.Car); //Агрим голема через и спавним его
            FindObjectOfType<GolemSpawn>().Spawn();
        }

        private IEnumerator dTime()
        {
            yield return new WaitForSeconds(2f); //Если мобы спавнятся во время выполнения этой корутины, то они не умирают. Надо как то отключить спавн
            Cam_3.SetActive(false);
            Cam_1.SetActive(true);
            animTrain.SetTrigger("next");
            backTrain.SetActive(false);
            AllData.SetStateGame(State.Roof);
            AllData.SetStateLvl(LvlState.Sky);

            FindObjectOfType<GolemSpawn>().Spawn();
        }
    }
}
