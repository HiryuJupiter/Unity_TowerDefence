using UnityEngine;
using System.Collections;

public abstract class PfxBase : PoolObject
{
    [SerializeField] private float timeBeforeDestroy = 1f;


    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
        
    public override void Respawned()
    {
        ps.Play();
        StartCoroutine(DelayBeforeDestroy());
    }

    IEnumerator DelayBeforeDestroy()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        ReturnToPool();
    }
}