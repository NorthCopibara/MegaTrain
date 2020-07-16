using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Pool;

namespace Qbik.Game.EnemyGame
{
    public class Robot : EnemyCharacter
    {
        protected override void ChekDeth(int damage)
        {
            if (healthBar != null)
                healthBar.ApllyDamage(damage);

            if (charHealth <= 0)
            {
                AllData.AddExp(exp);
                GetComponent<EnemyAI>().DethEnemy();
                StartCoroutine(DehtRobo());
            }
        }

        private IEnumerator DehtRobo()
        {
            yield return new WaitForSeconds(timeDeath);
            if (particleDeath != null)
            {
                particleDeath.Play();
            }
            yield return new WaitForSeconds(0.2f);
            ManagerPool.DeSpawn(PoolType.Robot, this.gameObject);
        }
    }
}
