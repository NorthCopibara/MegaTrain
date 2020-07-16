using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Game.ZoneGame.StageGame;

namespace Qbik.Game.EnemyGame
{
    public class Golem : EnemyCharacter
    {
        private GameObject buttonNextLvl;

        public void InitGolem(GameObject obj)
        {
            buttonNextLvl = obj;
        }

        protected override void ChekDeth(int damage)
        {
            if (healthBar != null)
                healthBar.ApllyDamage(damage);

            if (charHealth <= 0)
            {
                GetComponent<EnemyAI>().DethEnemy();
                AllData.AddExp(exp);
                StartCoroutine(DehtGolem());
            }
        }

        private IEnumerator DehtGolem()
        {
            yield return new WaitForSeconds(timeDeath);
            if (particleDeath != null)
            {
                particleDeath.Play();
            }
            yield return new WaitForSeconds(0.5f);
            #region SpawnTP
            GameObject tp = Resources.Load<GameObject>("Models/Prefabs/Character/TPObj") as GameObject;
            GameObject rek = Instantiate(tp, transform.position, Quaternion.identity);
            NextZone zone = rek.GetComponent<NextZone>();
            if (zone != null && buttonNextLvl != null)
            {
                zone.Init(buttonNextLvl);
            }
            else
            {
                //Poshol nafig
            }
            #endregion

            Destroy(gameObject);
        }
    }
}
