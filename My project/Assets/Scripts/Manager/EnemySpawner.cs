using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        ResetSpawner();
        StartSpawning();
    }

    public void ResetSpawner()
    {
        spawnCount = defaultSpawnCount + ((spawnCountMultiplier - 1) * multiplierIncreaseCount);
        totalKillWave = 0;
    }
    
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public void StopSpawning()
    {
        if (isSpawning)
        {
            isSpawning = false;
            StopCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(5f);

            for (int i = 0; i < spawnCount; i++)
            {
                Enemy newEnemy = Instantiate(spawnedEnemy);
                newEnemy.enemySpawner = this;

                combatManager.totalEnemies++;
                totalKillWave++;

                yield return new WaitForSeconds(spawnInterval);
            }

            while (totalKillWave > 0)
            {
                yield return null;
            }
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave--;

        if (totalKillWave <= 0)
        {
            combatManager.CheckWaveCompletion();
        }

        if (totalKill % minimumKillsToIncreaseSpawnCount == 0)
        {
            spawnCountMultiplier++;
        }
    }
}
