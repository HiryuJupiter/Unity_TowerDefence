using UnityEngine;
using System.Collections;

public class TowerAmmunitionManager : MonoBehaviour
{
    public static TowerAmmunitionManager Instance;

    [Header("Prefabs")]
    [SerializeField] GameObject basicBullet;

    Pool pool_basicBullet;

    public GameObject GetHomingLinearBullet ()
    {
        return pool_basicBullet.Spawn();
    }
}