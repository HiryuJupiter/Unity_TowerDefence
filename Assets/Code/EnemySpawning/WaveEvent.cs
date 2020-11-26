using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveEvent
{
    //Exposed variables
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private int pathIndex;
    [SerializeField] private int amount;

    //Public properties
    public EnemyTypes EnemyType => enemyType;
    public int PathIndex => pathIndex;
    public int Amount => amount;
}