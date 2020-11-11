using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnInterval = 10f;
    [SerializeField] Vector3 spawnDirection;

    //Status
    float spawnTimer;
    bool spawning;

    //Cache
    Quaternion spawnRotation;

    #region MonoBehavior
    void Awake()
    {
        instance = this;
        spawnRotation = Quaternion.Euler(spawnDirection);
    }

    #endregion

    #region Public
    public void StartSpawning()
    {
        StartCoroutine(DoSpawnSequence());
    }

    public void StopSpawning()
    {
        spawning = false;
    }
    #endregion

    #region Private
    IEnumerator DoSpawnSequence()
    {
        spawning = true;
        yield return new WaitForSeconds(spawnInterval);

        //Endless loop to spawn targets at a certain interval
        while (spawning)
        {
            SpawnTarget();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTarget()
    {
        //Instantiate prefab at a random location.
        Instantiate(prefab, spawnPoint.position, spawnRotation);
    }
    #endregion
}
