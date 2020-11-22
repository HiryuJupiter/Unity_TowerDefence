using System.Collections;
using UnityEngine;

public abstract class Enemy : PoolObject
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
    GameManager gm;

    public float Reward => reward;
    public bool Alive => hp > 0;

    protected virtual void Start()
    {
        gm = GameManager.Instance;
    }

    protected virtual void Update()
    {
        MoveAlongPath();
    }

    public void Initialize(Path path)
    {
        this.path = path;
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
            ReturnToPool();
        }
    }

    void ReachedCurrentWaypoint ()
    {
        if (++waypointIndex  >= path.WaypointCount)
        {
            gm.ReduceLife();
            reachedEnd = true;
            ReturnToPool();
        }
        else
        {
            nextWaypointPos = path.GetWaypointPosition(waypointIndex);
            transform.rotation = Quaternion.LookRotation(nextWaypointPos - transform.position, Vector3.up);
        }
    }

    void MoveAlongPath()
    {
        if (!reachedEnd && ArrivedAtWaypoint)
        {
            ReachedCurrentWaypoint();
        }
    }

    bool ArrivedAtWaypoint => Vector2.SqrMagnitude(transform.position - nextWaypointPos) < 1f;
}
