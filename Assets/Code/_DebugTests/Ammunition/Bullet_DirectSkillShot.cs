//using UnityEngine;
//using System.Collections;

//public class Bullet_DirectSkillShot : AmmunitionBase
//{
//    [SerializeField] float travelDuration = 1f;

//    Vector3 targetPos;

//    #region MonoBehavior
//    void OnTriggerEnter2D(Collider2D col)
//    {
//        if (IsEnemyCollider(col))
//        {
//            HitsTarget();
//        }
//    }
//    #endregion

//    protected override void Shoot()
//    {
//        targetPos = enemyTrans.position;
//        StartCoroutine(BulletTravel());
//    }

//    protected IEnumerator BulletTravel()
//    {
//        for (float t = 0; t < travelDuration; t += Time.deltaTime)
//        {
//            transform.position = Vector3.Lerp(startingPos, targetPos, t / travelDuration);
//            yield return null;
//        }
//        HitsTarget();
//    }

//    //protected IEnumerator HomingShoot ()
//    //{
//    //    while (enemy.Alive)
//    //    {
//    //        transform.rotation = RotationTowardsEnemy();
//    //        Vector3 move = transform.forward * Time.deltaTime * speed;
//    //        transform.position += move;
//    //        yield return null;
//    //    }
//    //}

//    //protected IEnumerator UntargetedStraightShot(float aliveDuration)
//    //{
//    //    for (float t = 0; t < aliveDuration; t += Time.deltaTime)
//    //    {
//    //        transform.position += transform.forward * Time.deltaTime * speed;
//    //        yield return null;
//    //    }
//    //    Detonate();
//    //}

//    //protected IEnumerator UntargetedDirectionalShot(float travelDuration)
//    //{
//    //    for (float t = 0; t < travelDuration; t += Time.deltaTime)
//    //    {
//    //        transform.position = Vector3.Lerp(startingPos, , t)transform.forward* Time.deltaTime* speed;
//    //        yield return null;
//    //    }
//    //    Detonate();
//    //}
//}