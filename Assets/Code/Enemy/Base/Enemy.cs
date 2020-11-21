using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int hp = 1;
    [SerializeField] int reward = 100;

    //Status
    int waypointIndex;
    Vector3 nextWaypointPos;
    bool reachedEnd;

    //Reference
    Path path;
    Pool pool;
    GameManager gm;

    public float Reward => reward;

    void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        if (!reachedEnd && ArrivedAtWaypoint)
        {
            ReachedCurrentWaypoint();
        }
    }

    public void Initialize(Pool pool, Path path)
    {
        this.path = path;
        this.pool = pool;
        reachedEnd = false;

        transform.position = path.GetWaypointPosition(0);

        ReachedCurrentWaypoint();
    }

    public void TakeDamage (int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gm.AddMoney(reward);
            Despawn();
        }
    }

    public void Despawn()
    {
        pool.Despawn(gameObject);
    }

    void ReachedCurrentWaypoint ()
    {
        if (++waypointIndex  >= path.WaypointCount)
        {
            gm.ReduceLife();
            Despawn();
        }
        else
        {
            nextWaypointPos = path.GetWaypointPosition(waypointIndex);
            transform.rotation = Quaternion.LookRotation(nextWaypointPos - transform.position, Vector3.up);
        }
    }

    bool ArrivedAtWaypoint => Vector2.SqrMagnitude(transform.position - nextWaypointPos) < 1f;
}
