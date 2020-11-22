using UnityEngine;
using System.Collections;

public abstract class AmmunitionBase : PoolObject
{
    //Reference
    protected Enemy enemy;
    protected LayerMask enemyLayer;

    //Cache 
    protected Transform enemyTrans;
    protected Vector3 startingPos;

    public virtual void Activate (Enemy enemy, Vector3 startingPos)
    {
        enemyLayer = Settings.Instance.EnemyLayer;

        this.enemy = enemy;
        enemyTrans = enemy.transform;

        this.startingPos = startingPos;
        transform.position = startingPos;
        Shoot();
    }

    protected abstract void Shoot ();

    protected Quaternion RotationTowardsEnemy()
    {
        Vector3 d = enemy.transform.position - transform.position;
        return Quaternion.LookRotation(d, Vector3.up);
    }

    protected Vector3 DirectionTowardsEnemy ()
    {
        return (enemy.transform.position - transform.position).normalized;
    }

    protected void HitsTarget ()
    {
        ReturnToPool();
        //Turn off trailRenderer
    }

    protected bool IsEnemyCollider(Collider2D col) => enemyLayer == (enemyLayer | 1 << col.gameObject.layer);


    //protected void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if ()
    //}
}