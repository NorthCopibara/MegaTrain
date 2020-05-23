using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Qbik.Static.Data;
using Asset.Scripts.Qbik.Static.Pool;

public class NextZone : MonoBehaviour
{
    private GameObject buttonNextLvl;
    private ButtonNextZone but; //Мусор
    private bool last;

    public void Init(GameObject buttonNextLvl)
    {
        this.buttonNextLvl = buttonNextLvl;
    }

    public void Next() 
    {
        //Убиваем всех противников
        foreach (GameObject x in AllData.PoolActivEnemy) 
        {
            ManagerPool.DeSpawn(PoolType.Robot, x);
        }
        AllData.ClearEnemyActiv();
        AllData.UpCar();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            buttonNextLvl.SetActive(true);

            if (but == null) 
            {
                but = buttonNextLvl.GetComponent<ButtonNextZone>();
                but.Init(collision.transform, this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buttonNextLvl.SetActive(false);
        }
    }
}
