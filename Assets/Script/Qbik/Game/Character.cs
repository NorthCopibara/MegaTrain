using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Asset.Scripts.Qbik.Static.Pool;
using Assets.Scripts.Qbik.Static.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour, ITakeDamage
{
    private int _charHealth;
    private int _charArmor;
    private int _exp;

    private float timeDeath;

    private GameObject buttonNextLvl;

    [SerializeField] private List<GameObject> _playerCam;

    [SerializeField] private TextMeshProUGUI _lvlText;
    [SerializeField] private HealthBar _healthBar;


    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private ParticleSystem _particleDeath;

    [SerializeField] private bool robot;
    [SerializeField] private bool player;
    [SerializeField] private bool golem;
    [SerializeField] private bool superGolem;

    public List<GameObject> ReCam()
    {
        return _playerCam;
    }

    public void InitGolem(GameObject obj) 
    {
        buttonNextLvl = obj;
    }

    public void Initialized(CharacterData dataInit) 
    {
        if (_lvlText != null)
            _lvlText.text = dataInit._lvl.ToString();

        timeDeath = dataInit._timeDeath;

        _exp = dataInit._exp;
        _charHealth = dataInit._health;
        _charArmor  = dataInit._armor;

        if (_healthBar != null)
        {
            Slider sl = _healthBar.GetComponent<Slider>();
            sl.maxValue = _charHealth;
            sl.value = _charHealth;
        }
    }

    public void TakeDamage(AttackData attack, int num)
   {
        VisualDamage();
        int damage = 0;

        if (player)
        {
            damage = attack._damage[0] / _charArmor;
        }
        else 
        {
             damage = attack._damage[num - 1] / _charArmor;
        }
        _charHealth -= damage;

        ChekDeth(damage);
    }

    private void VisualDamage()
    {
        if( _particle!= null)
            _particle.Play();
    }


    private void ChekDeth(int damage) 
    {
        if (_charHealth <= 0)
        {
            if (robot) 
            {
                AllData.AddExp(_exp);
                GetComponent<EnemyAI>().DethEnemy();
                StartCoroutine(DehtRobo());
            }
            if (superGolem) 
            {
                GetComponent<EnemyAI>().DethEnemy();
                AllData.SetStateGame(State.Load);
                AllData.SetStateLvl(LvlState.Load);
                StartCoroutine(DehtSuperGolem());
            }
            if (golem)
            {
                GetComponent<EnemyAI>().DethEnemy();
                AllData.AddExp(_exp);
                StartCoroutine(DehtGolem());
            }
            if (player)
            {
                GetComponent<PlayerController>().DeathPlayer();
                AllData.SetStateGame(State.Load);
                AllData.SetStateLvl(LvlState.Load);
                AllData.ClearLvl();
                ManagerPool.Dispose();
                StartCoroutine(PlayerDeath());
            }
        }
        if (_healthBar != null)
            _healthBar.ApllyDamage(damage);
    }

    private IEnumerator PlayerDeath() 
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ScenData>().iDeath();
    }

    private IEnumerator DehtSuperGolem()
    {
        yield return new WaitForSeconds(timeDeath);
        if (_particleDeath != null)
        {
            _particleDeath.Play();
        }
        yield return new WaitForSeconds(2f);
        AllData.ClearLvl();
        ManagerPool.Dispose();
        AllData.EndGame.SetActive(true);
    }
    
    private IEnumerator DehtGolem()
    {
        yield return new WaitForSeconds(timeDeath);
        if (_particleDeath != null)
        {
            _particleDeath.Play();
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

    private IEnumerator DehtRobo() 
    {
        yield return new WaitForSeconds(timeDeath);
        if (_particleDeath != null) 
        {
            _particleDeath.Play();
        }
        yield return new WaitForSeconds(0.2f);
        ManagerPool.DeSpawn(PoolType.Robot, this.gameObject);
    }

}

public struct CharacterData 
{
    public int _exp;
    public int _lvl;
    public int _health;
    public int _armor;

    public float _timeDeath;
}
