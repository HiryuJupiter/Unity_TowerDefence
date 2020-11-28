using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float WaveInterval = 5f;

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
    public List<Transform> allEnemyPositions { get; private set; }

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
    //    GUI.Label(new Rect(20, 300, 900, 20), " Enemy spawner AllEnemies.count " + allEnemyPositions.Count);
    //    GUI.Label(new Rect(20, 320, 900, 20), " too many arms count " + pools[EnemyTypes.TooManyArms].actives.Count);
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
        pf.GetComponent<Enemy>().SetPath(paths[pathIndex]);
    }

    public void SpawnImmediately ()
    {
        spawnDelay = 0f;
    }

    public void UpdateAllEnemies ()
    {
        //Cache all enemy positions
        allEnemyPositions = pools.SelectMany(pool => pool.Value.actives).Select(go => go.transform).ToList();
    }

    public bool TryGetClosestEnemyToPosition (Vector3 position, out Enemy closestEnemy)
    {
        //Loop thorugh all enemies and find the closest one
        closestEnemy = null;
        if (allEnemyPositions.Count > 0)
        {
            Transform closestTrans = allEnemyPositions[0];
            float closestDist = Vector3.SqrMagnitude(position - closestTrans.position);

            for (int i = 1; i < allEnemyPositions.Count; i++)
            {
                float d = Vector3.SqrMagnitude(position - allEnemyPositions[i].position);
                if (d < closestDist)
                {
                    closestDist = d;
                    closestTrans = allEnemyPositions[i];
                }
            }

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
        gm.StartWave();

        //Start wave and pass in a Action callback anonymous method
        waves[waveIndex].StartWave(() => WaveComplete(), this);
    }

    private void WaveComplete()
    {
        waveIndex = ++waveIndex >= waves.Count ? 0 : waveIndex;

        StartCoroutine(DelayThenSpawnWave());

        //if (++waveIndex < waves.Count)
        //{
        //    StartCoroutine(DelayThenSpawnWave());
        //}
        //else
        //{
        //    waves = 0;
        //    //AllWavesFinished = true;
        //}
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