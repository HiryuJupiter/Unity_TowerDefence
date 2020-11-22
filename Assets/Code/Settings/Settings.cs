using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    //Lazy singlton
    public static Settings Instance;

    //Exposed variables
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask platformLayer;

    //Properties
    public LayerMask PlatformLayer => platformLayer;
    public LayerMask EnemyLayer => enemyLayer;

    void Awake()
    {
        Instance = this;
    }
}
