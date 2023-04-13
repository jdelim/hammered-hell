using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = .25f;
    public float hitForce = 100f; // amount of force that hits an object with rigidbody
    public Transform gunEnd; // empty gameobject that marks the position at the end of the gun where bullet begins
    public float shotDuration = 0.05f;

    //private WaitForSeconds shotDuration = new WaitForSeconds(.07f); // how long we want (bullet trail?) to stay visible
    private LineRenderer laserLine; // takes an array of 2 or more points and draws a straight line between each one
    private float nextFire; // hold the time in which the player will be allowed to fire again after firing

    public Camera playerCamera;

    void Awake() 
    {
        laserLine = GetComponent<LineRenderer>();
        //playerCamera = GetComponentInParent<Camera>(); 
    }

    void Update()
    {
        nextFire += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && nextFire > fireRate)
        {
            nextFire = 0;
            laserLine.SetPosition(0, gunEnd.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, range))
            {
                laserLine.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject); // destroys gameobject, will update later to decrease enemy health
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin+(playerCamera.transform.forward * range));
            }
            StartCoroutine(ShootGun());
        }
    }

    IEnumerator ShootGun()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(shotDuration);
        laserLine.enabled = false;
    }
}
// if (Input.GetButtonDown("Fire1"))
        // {
        //     RaycastHit hit;
        //     if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        //     {
        //         Health enemyHealth = hit.transform.GetComponent<Health>();
        //         if (enemyHealth != null)
        //         {
        //             enemyHealth.amount -= damage;
        //             if (enemyHealth.amount <= 0f)
        //             {
        //                 enemyHealth.onDeath.Invoke();
        //             }
        //         }
        //     }
        // }