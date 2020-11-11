using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp = 1;
    [SerializeField] float moveSpeed;
    [SerializeField] int moneyReward = 1;


    public void Initialize()
    {
    }

    public void TakeDamage (int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die ()
    {
        Destroy(gameObject);
    }
}
