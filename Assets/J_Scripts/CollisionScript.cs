using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public Health enemyHealth;
    public float damage = 10f;
    public MeleeController mc;

    void OnTriggerEnter(Collider other)
    {
        if (mc.IsAttacking)
        {
            enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.amount -= damage;
            }
        }
    }
}
