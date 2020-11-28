using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] int damage = 5;

    private LayerMask enemyLayer;

    protected void Awake()
    {
        enemyLayer = CharacterSettings.instance.EnemyLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyLayer == (enemyLayer | 1 << other.gameObject.layer))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}