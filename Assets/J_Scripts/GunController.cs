using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunController : MonoBehaviour
{
    public int bullets;
    public int maxAmmo;
    public int reloadTime;
    public bool canFire;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public bool isReloading;
    //private WaitForSeconds reloadDuration = new WaitForSeconds();
    public float damage = 10f;
    public float range = 5.0f;
    public float fireRate = .25f;
    public float hitForce = 100f; // amount of force that hits an object with rigidbody
    public Transform gunEnd; // empty gameobject that marks the position at the end of the gun where bullet begins
    public float shotDuration = 0.05f;

    //private WaitForSeconds shotDuration = new WaitForSeconds(.07f); // how long we want (bullet trail?) to stay visible
    private LineRenderer laserLine; // takes an array of 2 or more points and draws a straight line between each one
    private float nextFire; // hold the time in which the player will be allowed to fire again after firing

    public Camera playerCamera;
    
    public Health enemyHealth;

    void Awake() 
    {
        laserLine = GetComponent<LineRenderer>();
        canFire = true;
    }

    void Update()
    {
        nextFire += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && bullets != maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetButtonDown("Fire1") && nextFire > fireRate && bullets > 0 && canFire)
        {
            nextFire = 0;
            laserLine.SetPosition(0, gunEnd.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            // if raycast hits an enemy
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, this.range))
            {
                laserLine.SetPosition(1, hit.point);
                enemyHealth = hit.transform.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.amount -= damage;
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin+(playerCamera.transform.forward * this.range));
            }
            bullets = bullets - 1;
            StartCoroutine(ShootGun());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        AudioSource ac2 = GetComponent<AudioSource>();
        ac2.PlayOneShot(reloadSound);
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        bullets = maxAmmo;
        canFire = true;
        isReloading = false;
    }

    IEnumerator ShootGun()
    {
        AudioSource ac1 = GetComponent<AudioSource>();
        ac1.PlayOneShot(shootSound);
        laserLine.enabled = true;
        yield return new WaitForSeconds(shotDuration);
        laserLine.enabled = false;
    }
}