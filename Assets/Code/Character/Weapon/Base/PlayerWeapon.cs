using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
    private LayerMask enemyLayer;

    protected void Awake()
    {
        enemyLayer = CharacterSettings.instance.EnemyLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayer == (enemyLayer | 1 << other.gameObject.layer))
        {
            other.GetComponent<Enemy>().TakeDamage(1);
        }
    }
}