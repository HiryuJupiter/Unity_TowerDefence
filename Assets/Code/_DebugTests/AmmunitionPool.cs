using UnityEngine;
using System.Collections;

public class AmmunitionPool : MonoBehaviour
{
    //Lazy singleton
    public static AmmunitionPool Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject basicBullet;
    private Pool pool_basicBullet;

    public GameObject GetHomingLinearBullet ()
    {
        return pool_basicBullet.Spawn();
    }
}