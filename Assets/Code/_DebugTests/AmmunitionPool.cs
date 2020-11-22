using UnityEngine;
using System.Collections;

public class AmmunitionPool : MonoBehaviour
{
    public static AmmunitionPool Instance;

    [Header("Prefabs")]
    [SerializeField] GameObject basicBullet;

    Pool pool_basicBullet;

    public GameObject GetHomingLinearBullet ()
    {
        return pool_basicBullet.Spawn();
    }
}