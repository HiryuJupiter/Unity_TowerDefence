using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float WaveInterval = 6f;

    //Lazy singleton
    public static EnemySpawner instance;

    [SerializeField] private Path[] paths;
    [SerializeField] private GameObject pf_TooManyArms;
    [SerializeField] private List<Wave> waves;
    private Dictionary<EnemyTypes, Pool> pools;

    //Status
    private int waveIndex;
    private float spawnDelay;

    //Reference
    private GameManager gm;

    //Property
    public bool AllWavesFinished { get; private set;}
    public List<Transform> AllEnemies { get; private set; }

    #region MonoBehavior
    private void Awake()
    {
        //Lazy singleton
        instance = this;

        //Initialize variables
        pools = new Dictionary<EnemyTypes, Pool>()
        {
            { EnemyTypes.TooManyArms, new Pool(pf_TooManyArms)},
        };
    }
    private void Start()
    {
        //Reference
        gm = GameManager.Instance;
        StartLevel();
    }

    private void Update()
    {
        UpdateAllEnemies();
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(20, 500, 200, 20), "AllEnemies.count " + AllEnemies.Count());
    //}
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

    public void UpdateAllEnemies ()
    {
        AllEnemies = pools.SelectMany(pool => pool.Value.actives).Select(go => go.transform).ToList();
    }

    public bool TryGetClosestEnemyToPositionBad (Vector3 position, out Enemy closestEnemy)
    {
        closestEnemy = null;
        if (AllEnemies.Count > 0)
        {
            Transform closestTrans = AllEnemies[0];
            float closestDist = Vector3.SqrMagnitude(position - closestTrans.position);

            foreach (var e in AllEnemies)
            {
                float d = Vector3.SqrMagnitude(position - e.position);
                if (d < closestDist)
                {
                    closestDist = d;
                    closestTrans = e;
                }
            }

            closestEnemy = closestTrans.GetComponent<Enemy>();
            return true;
        }
        return false;
    }

    public bool TryGetClosestEnemyToPosition2(Vector3 position, out Enemy closestEnemy)
    {
        closestEnemy = null;
        if (AllEnemies.Count > 0)
        {
            float closestDist = float.MaxValue;
            Transform closestTrans = AllEnemies[0];


            closestEnemy = closestTrans.GetComponent<Enemy>();
            return true;
        }
        return false;
    }
    #endregion

    #region Private
    private void SpawnWave()
    {
        //Tell gameManager we're going into a new wave
        gm.StartWave(waveIndex + 1);

        //Start wave and pass in a Action callback anonymous method
        waves[waveIndex].StartWave(() => WaveComplete(), this);
    }

    private void WaveComplete()
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

    private IEnumerator DelayThenSpawnWave()
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