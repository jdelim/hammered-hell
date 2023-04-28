using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 0.01f;
    public float fireRate = .25f;
    public float hitForce = 100f; // amount of force that hits an object with rigidbody
    public Transform gunEnd; // empty gameobject that marks the position at the end of the gun where bullet begins
    public float shotDuration = 0.05f;

    private LineRenderer[] laserLines = new LineRenderer[9]; // an array of line renderers to draw the raycasts
    private float nextFire; // hold the time in which the player will be allowed to fire again after firing

    public Camera playerCamera;

    void Awake()
    {
        for (int i = 0; i < laserLines.Length; i++)
        {
            GameObject laserObject = new GameObject("Laser " + i);
            laserObject.transform.SetParent(transform);
            laserLines[i] = laserObject.AddComponent<LineRenderer>();
            laserLines[i].positionCount = 2;
            laserLines[i].startWidth = 0.05f;
            laserLines[i].endWidth = 0.05f;
            laserLines[i].enabled = false;
        }
    }

    void Update()
    {
        nextFire += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && nextFire > fireRate)
        {
            nextFire = 0;

            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            for (int i = 0; i < 9; i++)
            {
                float deviationX = Random.Range(-0.05f, 0.05f);
                float deviationY = Random.Range(-0.05f, 0.05f);

                Vector3 direction = playerCamera.transform.forward;
                direction.x += deviationX;
                direction.y += deviationY;

                // add spread to the direction vector
                direction = Quaternion.Euler(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0) * direction;


                laserLines[i].SetPosition(0, gunEnd.position);
                laserLines[i].SetPosition(1, gunEnd.position + direction * range);
                laserLines[i].enabled = true;

                if (Physics.Raycast(rayOrigin, direction, out hit, range))
                {
                    //Destroy(hit.transform.gameObject);
                }
            }

            StartCoroutine(DisableLasers());
        }
    }

    IEnumerator DisableLasers()
    {
        yield return new WaitForSeconds(shotDuration);
        foreach (LineRenderer laser in laserLines)
        {
            laser.enabled = false;
        }
    }
}
