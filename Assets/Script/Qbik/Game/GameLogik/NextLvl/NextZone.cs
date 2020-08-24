using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Pool;


namespace Qbik.Game.ZoneGame.StageGame
{
    public class NextZone : MonoBehaviour
    {
        private GameObject buttonNextLvl;
        private ButtonNextZone but; //Мусор

        public void Init(GameObject buttonNextLvl)
        {
            this.buttonNextLvl = buttonNextLvl;
        }

        public void Next()
        {
            //Убиваем всех противников
            foreach (GameObject x in Model.Enemies.PoolActivEnemy)
            {
                ManagerPool.DeSpawn(PoolType.Robot, x);
            }
            Model.Enemies.ClearEnemyActiv();
            Model.Game.UpCar();
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                buttonNextLvl.SetActive(true);

                if (but == null)
                {
                    but = buttonNextLvl.GetComponent<ButtonNextZone>();
                    but.Init(this);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                buttonNextLvl.SetActive(false);
            }
        }
    }
}
