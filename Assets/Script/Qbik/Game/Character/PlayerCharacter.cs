using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Pool;
using Qbik.Game.Data;

namespace Qbik.Game.PlayerGame
{
    public class PlayerCharacter :Character
    {
        [SerializeField] private List<GameObject> playerCam; //player

        public List<GameObject> PlayerCam => playerCam;

        public override void TakeDamage(AttackData attack, int numCombo)
        {
            VisualDamage();

            int damage = attack._damage[0] / charArmor; //У противников пока только 1 тип атаки
            charHealth -= damage;

            ChekDeth(damage);
        }

        protected override void ChekDeth(int damage)
        {
            if (healthBar != null)
                healthBar.ApllyDamage(damage);

            if (charHealth <= 0)
            {
                GetComponent<PlayerController>().DeathPlayer();
                Model.Game.SetStateGame(State.Load);
                Model.Game.SetStateLvl(LvlState.Load);
                Model.ClearLvl();
                ManagerPool.Dispose();
                StartCoroutine(PlayerDeath());
            }
        }

        private IEnumerator PlayerDeath()
        {
            yield return new WaitForSeconds(2f);
            FindObjectOfType<ScenData>().iDeath();
        }
    }
}
