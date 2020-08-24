using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Qbik.Static.Data;
using Qbik.Static.Data.Calculate;
using JokerGho5t.MessageSystem;

namespace Qbik.Game.ZoneGame.StageGame
{
    public class ButtonNextZone : MonoBehaviour
    {
        private NextZone next;

        public void Init(NextZone next)
        {
            this.next = next;
        }

        public void NextZone()
        {
            Calculate.InitLvlRobot(Model.Game.DataZone._lvlEnemyZone[0]); //Сделано только для оного перехода

            Model.Game.SetStateGame(State.Load);
            Model.Game.SetStateLvl(LvlState.Load);
            gameObject.GetComponent<Image>().enabled = false;
            StartCoroutine(TP());
        }

        private IEnumerator TP()
        {
            yield return new WaitForSeconds(1f);
            if (Model.Game.DataZone._maxCar - 1 > Model.Game.NumberCar)
            {
                next.Next();
                Message.Send("PlayerToSpawn");
            }
            else
            {
                next.Next();
                Message.Send("PlayerToLastSpawn");
            }
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
