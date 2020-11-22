using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveEvent
{
    //Exposed variables
    [SerializeField] EnemyTypes enemyType;
    [SerializeField] int pathIndex;
    [SerializeField] int amount;

    //Public properties
    public EnemyTypes EnemyType => enemyType;
    public int PathIndex => pathIndex;
    public int Amount => amount;
}