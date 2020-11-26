using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    //Lazy singlton
    public static Settings Instance;

    //Exposed variables
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask platformLayer;

    //Properties
    public LayerMask PlatformLayer => platformLayer;
    public LayerMask EnemyLayer => enemyLayer;

    private void Awake()
    {
        Instance = this;
    }
}
