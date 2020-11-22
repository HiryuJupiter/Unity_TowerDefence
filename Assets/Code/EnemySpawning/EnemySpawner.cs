using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const float WaveInterval = 6f;

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

    public bool AllWavesFinished { get; private set;}

    #region MonoBehavior
    void Awake()
    {
        instance = this;
        pools = new Dictionary<EnemyTypes, Pool>()
        {
            { EnemyTypes.TooManyArms, new Pool(pf_TooManyArms)},
        };
    }

    void Start()
    {
        gm = GameManager.Instance;
    }
    #endregion

    #region Public
    public void StartLevel()
    {
        SpawnWave();
    }

    public void SpawnEnemy(EnemyTypes enemyType, int pathIndex)
    {
        GameObject pf = pools[enemyType].Spawn();
        pf.GetComponent<Enemy>().Initialize(paths[pathIndex]);
    }

    public void SpawnImmediately ()
    {
        spawnDelay = 0f;
    }

    #endregion

    #region Private
    void WaveComplete()
    {
        if (++waveIndex < waves.Count)
        {
            StartCoroutine(DelayThenSpawnWave());
        }
        else
        {
            AllWavesFinished = true;
        }
    }

    IEnumerator DelayThenSpawnWave ()
    {
        spawnDelay = WaveInterval;
        while (spawnDelay > 0f)
        {
            spawnDelay -= Time.deltaTime;
            //ui.SetDelayTimer(spawnDelay/waveInterval);
            yield return null;
        }
        //ui.SetDelayTimer(0f);
        SpawnWave();
    }

    void SpawnWave()
    {
        gm.StartWave(waveIndex + 1);
        waves[waveIndex].StartWave(() => WaveComplete(), this);
    }
    #endregion
}