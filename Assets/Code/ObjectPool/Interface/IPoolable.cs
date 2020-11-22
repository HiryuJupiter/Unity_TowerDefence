using UnityEngine;
using System.Collections;

public abstract class PoolObject : MonoBehaviour
{
    protected Pool pool;
    
    public void SetPool (Pool pool)
    {
        this.pool = pool;
    }

    protected void ReturnToPool()
    {
        pool.Despawn(gameObject);
    }
}