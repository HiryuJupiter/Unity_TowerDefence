using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    [SerializeField] LayerMask enemyLayer;

    public LayerMask EnemyLayer => enemyLayer;

    void Awake()
    {
        Instance = this;
    }
}
