using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Qbik.Static.Data;

namespace Qbik.Game.StoryGame
{
    public class Story : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private int step = 1;

        public void Start()
        {
            Step(step);
            UpStep();
        }

        private void Step(int step)
        {
            anim.SetInteger("Step", step);
        }

        public void PushStep()
        {
            UpStep();
        }

        private IEnumerator FUK()
        {
            yield return new WaitForSeconds(5f);

            UpStep();
        }

        public void UpStep()
        {
            if (step == 5)
            {
                gameObject.SetActive(false); //Точка старта игры
                Model.Game.SetStateLvl(LvlState.Sky);
                return;
            }
            step++;
            Step(step);
            StartCoroutine(FUK());
        }
    }
}
