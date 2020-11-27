using UnityEngine;
using System.Collections;

public class SfxPlayer : MonoBehaviour
{
    public static SfxPlayer instance;
    [SerializeField] GameObject sfx_EnemyHurt;

    private void Awake()
    {
        instance = this;
    }

    public void Play_EnemyHurt()
    {
        SpawnSfxPrefab(sfx_EnemyHurt);
    }

    void SpawnSfxPrefab(GameObject pf)
    {
        Instantiate(pf, Vector3.zero, Quaternion.identity);
    }
}