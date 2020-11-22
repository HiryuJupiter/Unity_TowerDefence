using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const float WaveInterval = 6f;

    //Lazy singleton
    public static EnemySpawner instance;

    [SerializeField] Path[] paths;
    [SerializeField] GameObject pf_TooManyArms;
    [SerializeField] List<Wave> waves;

    Dictionary<EnemyTypes, Pool> pools;

    //Status
    int waveIndex;
    float spawnDelay;

    //Reference
    GameManager gm;

    //Property
    public bool AllWavesFinished { get; private set;}

    #region MonoBehavior
    void Awake()
    {
        //Lazy singleton
        instance = this;

        //Initialize variables
        pools = new Dictionary<EnemyTypes, Pool>()
        {
            { EnemyTypes.TooManyArms, new Pool(pf_TooManyArms)},
        };
    }

    void Start()
    {
        //Reference
        gm = GameManager.Instance;
        StartLevel();
    }
    #endregion

    #region Public
    public void StartLevel()
    {
        SpawnWave();
    }

    public void SpawnEnemy(EnemyTypes enemyType, int pathIndex)
    {
        //Spawn enemy from pool and initialize it
        GameObject pf = pools[enemyType].Spawn();
        pf.GetComponent<Enemy>().Initialize(paths[pathIndex]);
    }

    public void SpawnImmediately ()
    {
        spawnDelay = 0f;
    }

    #endregion

    #region Private
    void SpawnWave()
    {
        //Tell gameManager we're going into a new wave
        gm.StartWave(waveIndex + 1);

        //Start wave and pass in a Action callback anonymous method
        waves[waveIndex].StartWave(() => WaveComplete(), this);
    }

    void WaveComplete()
    {
        //When wave is completed, delay then spawn 
        if (++waveIndex < waves.Count)
        {
            StartCoroutine(DelayThenSpawnWave());
        }
        else
        {
            AllWavesFinished = true;
        }
    }

    IEnumerator DelayThenSpawnWave()
    {
        //Have a slight delay between waves. Ideally we have a timer that ticks down
        spawnDelay = WaveInterval;
        while (spawnDelay > 0f)
        {
            spawnDelay -= Time.deltaTime;
            //ui.SetDelayTimer(spawnDelay/waveInterval);
            yield return null;
        }
        //ui.SetDelayTimer(0f);
        //Once the time is up, spawn wave.
        SpawnWave();
    }
    #endregion
}