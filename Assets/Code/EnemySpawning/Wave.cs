using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    const float SpawnInterval = 0.5f;

    public List<WaveEvent> waveEvents;

    Action waveCompletedCallBack;
    EnemySpawner spawner;

    public void StartWave(Action waveCompletedCallBack, EnemySpawner spawner)
    {
        this.waveCompletedCallBack = waveCompletedCallBack;
        this.spawner = spawner;

        spawner.StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveEvents.Count; i++)
        {
            WaveEvent e = waveEvents[i];

            for (int j = 0; j < e.Amount; j++)
            {
                spawner.SpawnEnemy(e.EnemyType, e.PathIndex);
                yield return new WaitForSeconds(SpawnInterval);
            }
        }

        waveCompletedCallBack();
    }
}