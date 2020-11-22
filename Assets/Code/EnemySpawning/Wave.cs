using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    //Const
    const float SpawnInterval = 0.5f;

    //Expose wave sequences that can be edited in Inspector
    public List<WaveEvent> waveEvents;

    //Cache Callback
    Action waveCompletedCallBack;

    //Reference
    EnemySpawner spawner;

    public void StartWave(Action waveCompletedCallBack, EnemySpawner spawner)
    {
        //Cache reference
        this.waveCompletedCallBack = waveCompletedCallBack;
        this.spawner = spawner;

        //Start spawning wave
        spawner.StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        //Spawn each wave event (sub waves) one by one
        for (int i = 0; i < waveEvents.Count; i++)
        {
            WaveEvent e = waveEvents[i];

            for (int j = 0; j < e.Amount; j++)
            {
                spawner.SpawnEnemy(e.EnemyType, e.PathIndex);
                yield return new WaitForSeconds(SpawnInterval);
            }
        }

        //Once all waves are done, tell the caller we're done
        waveCompletedCallBack();
    }
}