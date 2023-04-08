using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera playerCamera;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
            {
                Health enemyHealth = hit.transform.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.amount -= damage;
                    if (enemyHealth.amount <= 0f)
                    {
                        enemyHealth.onDeath.Invoke();
                    }
                }
            }
        }
    }
}
