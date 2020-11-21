using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveEvent
{
    [SerializeField] EnemyTypes enemyType;
    [SerializeField] int pathIndex;
    [SerializeField] int amount;

    public EnemyTypes EnemyType => enemyType;
    public int PathIndex => pathIndex;
    public int Amount => amount;
}