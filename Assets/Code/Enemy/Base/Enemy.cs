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

    #region MonoBehavior
    protected virtual void Start()
    {
        gm = GameManager.Instance;
    }

    protected virtual void Update()
    {
        MoveAlongPath();
    }
    #endregion

    #region Public
    public void Initialize(Path path)
    {
        this.path = path;
        reachedEnd = false;
        waypointIndex = 1;

        transform.position = path.GetWaypointPosition(0);
        nextWaypointPos = CurrentWaypointPosition;
        transform.rotation = RotationToNext;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gm.AddMoney(reward);
            ReturnToPool();
        }
    }
    #endregion
    void MoveAlongPath()
    {
        if (!reachedEnd)
        {
            if (ArrivedAtWaypoint)
            {
                ReachedCurrentWaypoint();
            }
            else
            {
                Vector3 dir = Vector3.RotateTowards(transform.forward, DirectionToNext, 4f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

            }
        }
    }

    void ReachedCurrentWaypoint()
    {
        if (++waypointIndex >= path.WaypointCount)
        {
            Debug.Log("Reached end");
            gm.ReduceLife();
            reachedEnd = true;
            ReturnToPool();
        }
        else
        {
            nextWaypointPos = CurrentWaypointPosition;
            //transform.rotation = RotationToNext;
        }
    }

    Quaternion RotationToNext => Quaternion.LookRotation(DirectionToNext, Vector3.up);
    Vector3 CurrentWaypointPosition => path.GetWaypointPosition(waypointIndex);
    bool ArrivedAtWaypoint => Vector3.Distance(transform.position, nextWaypointPos) < 0.01f;
    Vector3 DirectionToNext => nextWaypointPos - transform.position;
}
