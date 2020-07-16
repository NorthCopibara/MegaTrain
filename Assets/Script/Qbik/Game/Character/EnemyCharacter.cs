using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qbik.Game.EnemyGame
{
    public class EnemyCharacter : Character
    {
        public override void TakeDamage(AttackData attack, int numCombo)
        {
            VisualDamage();

            int damage = attack._damage[numCombo - 1] / charArmor;
            charHealth -= damage;

            ChekDeth(damage);
        }

        protected override void ChekDeth(int damage)
        {
        }
    }
}
