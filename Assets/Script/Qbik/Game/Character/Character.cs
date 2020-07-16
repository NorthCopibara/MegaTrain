using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using Qbik.Game.GameUI.Character;

namespace Qbik.Game
{
    public abstract class Character : MonoBehaviour, ITakeDamage
    {
        protected int charHealth;
        protected int charArmor;
        protected int exp;

        protected float timeDeath;

        [SerializeField] protected TextMeshProUGUI lvlText; 
        [SerializeField] protected HealthBar healthBar; 


        [SerializeField] protected ParticleSystem particle;
        [SerializeField] protected ParticleSystem particleDeath;
       
        public void Initialized(CharacterData dataInit)
        {
            timeDeath = dataInit.timeDeath;

            exp = dataInit.exp;
            charHealth = dataInit.health;
            charArmor = dataInit.armor;

            if (lvlText != null)
                lvlText.text = dataInit.lvl.ToString();

            if (healthBar != null)
            {
                Slider sl = healthBar.GetComponent<Slider>();
                sl.maxValue = charHealth;
                sl.value = charHealth;
            }
        }

        public abstract void TakeDamage(AttackData attack, int numCombo);

        protected void VisualDamage()
        {
            if (particle != null)
                particle.Play();
        }

        protected abstract void ChekDeth(int damage);
    }
}

public struct CharacterData 
{
    public int exp;
    public int lvl;
    public int health;
    public int armor;

    public float timeDeath;
}
