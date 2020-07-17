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
                Model.Game.SetStateGame(State.Load);
                Model.Game.SetStateLvl(LvlState.Load);
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
            Model.ClearLvl();
            ManagerPool.Dispose();
            Model.Game.EndGame.SetActive(true);
        }
    }
}
