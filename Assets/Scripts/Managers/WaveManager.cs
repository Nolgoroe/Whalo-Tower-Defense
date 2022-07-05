using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{



    //[Header("Waves scriptable Objects")]
    //public WaveScriptableObject[] allWaves;

    [Header("General wave data")]
    public int waveSpawnDelay;
    public int timeDelaySpawnEnemy;
    public int waveNumAddFlyingEnemies;

    [Header("Ground enemies data")]
    public int groundEnemiesToSpawnThisWave;
    public int groundEnemySpeed;
    public int groundEnemyHealth;
    public bool hasFinishedSummoningGround;

    [Header("Flying enemies data")]
    public int flyingEnemiesToSpawnThisWave;
    public int flyingEnemySpeed;
    public int flyingEnemyHealth;
    public bool hasFinishedSummoningFlying;

    [Header("Data change between waves Ground Enemis")]
    public int addAmountGroundEnemies;
    public int addSpeedGroundEnemies;
    public int addHealthGroundEnemies;

    [Header("Data change between waves Flying Enemis")]
    public int addAmountFlyingEnemies;
    public int addSpeedFlyingEnemies;
    public int addHealthFlyingdEnemies;

    [Header("current Wave Data")]
    public int currentWaveID;
    public int groundEnemiesSpawned;
    public int flyingEnemiesSpawned;
    public bool doneSpawning;


    public IEnumerator CountdownNextWave(int spawnDelay)
    {
        while (spawnDelay > 0)
        {
            //ClassRefrencer.instance.UIManager.DisplaySystemMessages();
            ClassRefrencer.instance.announcementManager.UpdateText("Time till next wave: " + spawnDelay.ToString(), ClassRefrencer.instance.announcementManager._generalText);
            yield return new WaitForSeconds(1);
            spawnDelay--;
        }

        ClassRefrencer.instance.announcementManager.Show("Time till next wave: " + spawnDelay.ToString(), 0.9f, ClassRefrencer.instance.announcementManager._generalText);

        StartCoroutine(SpawnWave(currentWaveID));
    }

    private IEnumerator SpawnWave(int waveID)
    {
        while (groundEnemiesSpawned + flyingEnemiesSpawned < groundEnemiesToSpawnThisWave + flyingEnemiesToSpawnThisWave)
        {
            ChooseEnemyToSpawn();
            yield return new WaitForSeconds(timeDelaySpawnEnemy);

        }

        doneSpawning = true;

        CheckCanAdvanceWave();
    }

    private void ChooseEnemyToSpawn()
    {
        if(!hasFinishedSummoningFlying && !hasFinishedSummoningGround)
        {
            int random = Random.Range(0, 2);

            if(random == 1)
            {
                SpawnGroundEnemy();
            }
            else
            {
                SpawnFlyingEnemy();
            }

            return;
        }

        if (!hasFinishedSummoningFlying)
        {
            SpawnFlyingEnemy();
            return;
        }

        if (!hasFinishedSummoningGround)
        {
            SpawnGroundEnemy();
            return;
        }
    }

    private void SpawnGroundEnemy()
    {
        groundEnemiesSpawned++;

        if (groundEnemiesSpawned >= groundEnemiesToSpawnThisWave)
        {
            hasFinishedSummoningGround = true;
        }

        ClassRefrencer.instance.enemyManager.SpawnEnemy(EnemyTypes.GroundEnemy, groundEnemySpeed, groundEnemyHealth);
    }
    private void SpawnFlyingEnemy()
    {
        flyingEnemiesSpawned++;

        if(flyingEnemiesSpawned >= flyingEnemiesToSpawnThisWave)
        {
            hasFinishedSummoningFlying = true;
        }

        ClassRefrencer.instance.enemyManager.SpawnEnemy(EnemyTypes.FlyingEnemy, flyingEnemySpeed, flyingEnemyHealth);
    }


    [ContextMenu("CheckCanAdvanceWave")]
    public void CheckCanAdvanceWave()
    {
        if(doneSpawning && ClassRefrencer.instance.enemyManager.allEnemiiesInGame.Count <= 0)
        {
            SetDataNextWave();

            StartCoroutine(AdvanceToNextWave());
        }
    }

    private void SetDataNextWave()
    {
        groundEnemiesSpawned = 0;
        flyingEnemiesSpawned = 0;

        ClassRefrencer.instance.enemyManager.allEnemiiesInGame.Clear();

        doneSpawning = false;
        hasFinishedSummoningGround = false;


        groundEnemiesToSpawnThisWave += addAmountGroundEnemies;
        groundEnemySpeed += addSpeedGroundEnemies;
        groundEnemyHealth += addHealthGroundEnemies;
        currentWaveID++;


        if(currentWaveID >= waveNumAddFlyingEnemies)
        {
            hasFinishedSummoningFlying = false;

            flyingEnemiesToSpawnThisWave += addAmountFlyingEnemies;
            flyingEnemySpeed += addSpeedFlyingEnemies;
            flyingEnemyHealth += addHealthFlyingdEnemies;
        }
        else
        {
            hasFinishedSummoningFlying = true;
        }
    }

    private IEnumerator AdvanceToNextWave()
    {
        //ClassRefrencer.instance.UIManager.DisplaySystemMessages();
        ClassRefrencer.instance.announcementManager.Show("Wave Ended!", 1, ClassRefrencer.instance.announcementManager._generalText);

        yield return new WaitForSeconds(2);

        StartCoroutine(CountdownNextWave(waveSpawnDelay));

        Debug.LogError("done with coroutine");
    }
}
