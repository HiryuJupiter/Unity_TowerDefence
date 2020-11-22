using System.Collections;
using UnityEngine;

//Base class for enemies
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

    //Property
    public float Reward => reward;
    public bool Alive => hp > 0;

    #region MonoBehavior
    protected virtual void Start()
    {
        //Reference
        gm = GameManager.Instance;
    }

    protected virtual void Update()
    {
        //Every frame, update and run this method
        MoveAlongPath();
    }
    #endregion

    #region Public
    public void Initialize(Path path)
    {
        //Reset all values
        this.path = path;
        reachedEnd = false;
        waypointIndex = 1;

        //Set the initial waypoint position
        transform.position = path.GetWaypointPosition(0);
        nextWaypointPos = CurrentWaypointPosition;
        transform.rotation = RotationToNext;
    }

    public void TakeDamage(int damage)
    {
        //Reduce health. If runs out of hp, add money and return to pool
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
        //Move along path while haven't reached the end.
        if (!reachedEnd)
        {
            if (ArrivedAtWaypoint)
            {
                //When arrived at way point, then you've reached current waypoint. 
                ReachedCurrentWaypoint();
            }
            else
            {
                //Rotate slowly towards the target direction
                Vector3 dir = Vector3.RotateTowards(transform.forward, DirectionToNext, 4f * Time.deltaTime, 0.0f);
                transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
                //Move forward
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    void ReachedCurrentWaypoint()
    {
        //Increment waypoint
        if (++waypointIndex >= path.WaypointCount)
        {
            //If we reached the end of path and hits player base, then reduce life
            Debug.Log("Reached end");
            gm.ReduceLife();
            reachedEnd = true;
            ReturnToPool();
        }
        else
        {
            //If we haven't reached the last waypoint, then just get the waypoint position
            nextWaypointPos = CurrentWaypointPosition;
        }
    }

    //Expression body methods for self documenting code
    Quaternion RotationToNext => Quaternion.LookRotation(DirectionToNext, Vector3.up);
    Vector3 CurrentWaypointPosition => path.GetWaypointPosition(waypointIndex);
    bool ArrivedAtWaypoint => Vector3.Distance(transform.position, nextWaypointPos) < 0.01f;
    Vector3 DirectionToNext => nextWaypointPos - transform.position;
}
