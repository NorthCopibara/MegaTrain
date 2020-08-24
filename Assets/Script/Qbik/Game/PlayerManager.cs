using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JokerGho5t.MessageSystem;
using Qbik.Game.ZoneGame.StageGame;

public class PlayerManager 
{
    private Transform player;
    private AnimTP animTP;
    private Transform playerSpawn;
    private Transform playerLastSpawn;

    public PlayerManager(Transform player, Transform playerSpawn, Transform playerLastSpawn)
    {
        this.player = player;
        this.playerSpawn = playerSpawn;
        this.playerLastSpawn = playerLastSpawn;

        animTP = player.GetComponent<AnimTP>();

        Message.AddListener("PlayerToSpawn", PlayerToSpawn);
        Message.AddListener("PlayerToLastSpawn", PlayerToLastSpawn);
    }

    public void PlayerToSpawn() 
    {
        animTP.FirstStep();
        player.position = playerSpawn.position;
    }

    public void PlayerToLastSpawn()
    {
        animTP.LastStep();
        player.position = playerLastSpawn.position;
    }

    public void Destroy() 
    {
        Message.RemoveListener("PlayerToSpawn", PlayerToSpawn);
        Message.RemoveListener("PlayerToLastSpawn", PlayerToLastSpawn);
    }
}
