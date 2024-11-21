using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] public float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;

    public bool waveCleared = true;

    private void Update()
    {
        if (waveCleared)
            timer += Time.deltaTime;
        
        if (timer >= waveInterval && waveCleared)
        {
            StartWave();
            timer = 0;
        }
    }

    private void StartWave()
    {
        waveNumber++;
        waveCleared = false;
        totalEnemies = 0;

        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetSpawner();
            spawner.isSpawning = true;
        }
    }

    public void CheckWaveCompletion()
    {
        foreach (var spawner in enemySpawners)
        {
            if (spawner.totalKillWave > 0)
                return;
        }

        waveCleared = true;
    }
}