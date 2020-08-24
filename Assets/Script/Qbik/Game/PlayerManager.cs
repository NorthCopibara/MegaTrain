using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JokerGho5t.MessageSystem;
using Qbik.Game.ZoneGame.StageGame;

public class PlayerManager 
{
    private Transform player;
    private Transform playerSpawn;
    private Transform playerLastSpawn;

    private GameObject deathConvas;

    public PlayerManager(Transform player, Transform playerSpawn, Transform playerLastSpawn, GameObject deathConvas)
    {
        this.player = player;
        this.playerSpawn = playerSpawn;
        this.playerLastSpawn = playerLastSpawn;
        this.deathConvas = deathConvas;

        Message.AddListener("PlayerToSpawn", PlayerToSpawn);
        Message.AddListener("PlayerToLastSpawn", PlayerToLastSpawn);
        Message.AddListener("PlayerDeath", PlayerDeath);
    }

    public void PlayerToSpawn() 
    {
        Message.Send("AnimTPFirstStep");
        player.position = playerSpawn.position;
    }

    public void PlayerToLastSpawn()
    {
        Message.Send("AnimTPLastStep");
        player.position = playerLastSpawn.position;
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0;
        deathConvas.SetActive(true);
    }

    public void Destroy() 
    {
        Message.RemoveListener("PlayerToSpawn", PlayerToSpawn);
        Message.RemoveListener("PlayerToLastSpawn", PlayerToLastSpawn);
        Message.RemoveListener("PlayerDeath", PlayerDeath);
    }
}
