//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class BaseBuilding : MonoBehaviour
//{
//    const float EnemyUpdateDelay = 0.2f;

//    //Serialized fields
//    [Header("Reference")]
//    [SerializeField] Transform shootPoint;
//    [SerializeField] Transform TurretPivot;

//    [Header("Stats")]
//    [SerializeField] float startingRadius = 1;
//    [SerializeField] float startingAttackCooldown = 1;

//    //Reference
//    EnemySpawner enemySpawner;
//    Enemy closestEnemy;
//    TowerAmmunitionManager ammoManager;

//    //Status
//    float attackTimer;

//    //Cache
//    Collider2D collider;
//    Vector3 towerPosition;

//    //Property
//    public float AttackRadius { get; private set; }


//    #region MonoBehavior
//    void Awake()
//    {
//        collider = GetComponent<Collider2D>();
//        AttackRadius = startingRadius;
//    }

//    void Start()
//    {
//        ammoManager = TowerAmmunitionManager.Instance;
//    }

//    void Update()
//    {
//        UpdateTimers();
//    }

//    #endregion

//    #region Public 
//    public void Initialize(Platform platform)
//    {
//        towerPosition = transform.position;
//    }
//    #endregion

//    public Enemy GetClosestEnemy()
//    {
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(towerPosition, AttackRadius);

//        float shortestDist = AttackRadius * AttackRadius;

//        Enemy enemy = null;
//        foreach (Collider2D c in colliders)
//        {
//            if (c != collider)
//            {
//                enemy = c.GetComponent<Enemy>();
//                if (enemy != null)
//                {
//                    float dist = Vector3.SqrMagnitude(towerPosition - c.transform.position);
//                    if (dist < shortestDist)
//                    {
//                        shortestDist = dist;
//                    }
//                }
//            }
//        }
//        return enemy;
//    }

//    public List<Enemy> UpdateEnemiesInRange(Vector3 origin, float radius)
//    {
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, radius);
//        List<Enemy> enemiesInRange = new List<Enemy>();

//        foreach (Collider2D c in colliders)
//        {
//            if (c != collider)
//            {
//                Enemy enemy = c.GetComponent<Enemy>();
//                if (enemy != null)
//                {
//                    enemiesInRange.Add(enemy);
//                }
//            }
//        }
//        return enemiesInRange;
//    }

//    void UpdateTimers()
//    {
//        //Attack timer
//        attackTimer -= Time.deltaTime;
//        if (attackTimer < 0f)
//        {
//            Attack();
//        }
//    }

//    protected virtual void Attack()
//    {
//        closestEnemy = GetClosestEnemy();
//        if (closestEnemy != null)
//        {
//            TurretPivot.rotation = DirectionTowardsClosestEnemy();
//            GameObject bullet = ammoManager.GetHomingLinearBullet();
//            bullet.GetComponent<AmmunitionBase>().Activate(closestEnemy, shootPoint.position);
//        }
//    }

//    Quaternion DirectionTowardsClosestEnemy()
//    {
//        Vector3 d = closestEnemy.transform.position - towerPosition;
//        return Quaternion.LookRotation(d, Vector3.up);
//    }
//}
