using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Pool;

namespace Qbik.Game.EnemyGame
{
    public class SuperGolem : EnemyCharacter
    {
        protected override void ChekDeth(int damage)
        {
            if (healthBar != null)
                healthBar.ApllyDamage(damage);

            if (charHealth <= 0)
            {
                GetComponent<EnemyAI>().DethEnemy();
                AllData.SetStateGame(State.Load);
                AllData.SetStateLvl(LvlState.Load);
                StartCoroutine(DehtSuperGolem());
            }
        }

        private IEnumerator DehtSuperGolem()
        {
            yield return new WaitForSeconds(timeDeath);
            if (particleDeath != null)
            {
                particleDeath.Play();
            }
            yield return new WaitForSeconds(2f);
            AllData.ClearLvl();
            ManagerPool.Dispose();
            AllData.EndGame.SetActive(true);
        }
    }
}
