using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Qbik.Static.Data;

public class ButtonNextZone : MonoBehaviour
{
    private Transform player;
    private Transform playerSpawn;
    private Transform playerLastSpawn;
    private NextZone next;

    public void Init(Transform player, NextZone next) 
    {
        this.player = player;
        this.next = next;
        playerSpawn = AllData.CSData.playerSpawn;
        playerLastSpawn = AllData.CSData.playerLastSpawn;
    }

    public void NextZone() 
    {
        AllData.SetStateGame(State.Load);
        AllData.SetStateLvl(LvlState.Load);
        gameObject.GetComponent<Image>().enabled = false;
        StartCoroutine(TP());
    }

    private IEnumerator TP() 
    {
        yield return new WaitForSeconds(1f);
        if (AllData.DataZone._maxCar - 1 > AllData.NumberCar)
        {
            next.Next();
            player.GetComponent<AnimTP>().FirstStep();
            player.transform.position = playerSpawn.position;
        }
        else 
        {
            next.Next();
            player.GetComponent<AnimTP>().LastStep();
            player.transform.position = playerLastSpawn.position;
        }
        gameObject.GetComponent<Image>().enabled = true;
    }
}
