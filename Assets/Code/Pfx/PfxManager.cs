using UnityEngine;
using System.Collections;

public class PfxManager : MonoBehaviour
{
    public static PfxManager Instance;

    [SerializeField] private GameObject pfx_EnemyHurt;

    private Pool pool_EnemyHurt;

    void Awake()
    {
        Instance = this;
        pool_EnemyHurt = new Pool(pfx_EnemyHurt);
    }

    public void SpawnPfx_EnemyHurt (Vector3 pos)
    {
        pool_EnemyHurt.Spawn(pos);
    }
}