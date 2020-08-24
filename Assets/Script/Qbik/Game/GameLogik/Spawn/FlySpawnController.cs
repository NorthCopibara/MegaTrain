using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Qbik.Static.Data;
using Qbik.Static.Data.Calculate;
using Qbik.Game;

public class FlySpawnController
{
    private bool dSpawn;
    private float timeSpawn;
    private Animator zippen;

    public FlySpawnController(Animator zippen, float timeSpawn)
    {
        this.zippen = zippen;
        this.timeSpawn = timeSpawn;

        ControlSystem.update += SpawnUpdatet;
    }

    private void SpawnUpdatet()
    {
        if (Model.Game.Lvl == LvlState.Sky)
        {
            SetFlySpawn();
        }
        else if (dSpawn)
        {
            CoroutinesManager.myCoroutinesManager.StopAllCoroutines();
            zippen.SetTrigger("Idle");
            dSpawn = false;
        }
    }

    private IEnumerator TimeSpawn()
    {
        yield return new WaitForSeconds(timeSpawn);
        Model.Game.UpSpawn();
        dSpawn = false;
    }

    private void SetFlySpawn()
    {
        if (!dSpawn)
        {
            if (Model.Game.NumberSpawn != 0)
                Calculate.UpRobot();

            zippen.SetTrigger("PlaySpawn");
            dSpawn = true;
            CoroutinesManager.myCoroutinesManager.MyStartCoroutine(TimeSpawn());
        }
    }

    public void Destroy()
    {
        ControlSystem.update -= SpawnUpdatet;
    }
}
