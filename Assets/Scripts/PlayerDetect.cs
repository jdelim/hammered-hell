using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public float raycastDistance = 10f;
    public Color greenBeam = new Color(0,1,0);
    public Color redBeam = new Color(1,0,0);

    void Update()
    {
        // Shoot a raycast in the forward direction of the enemy
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // If the raycast hits an object with the "Player" tag, print "Player detected" to the console
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected");
                Debug.DrawRay(transform.position, transform.forward * hit.distance, redBeam);
            }

        // Draw a line from the enemy's position to the point where the raycast hits an object
        Debug.DrawRay(transform.position, transform.forward * hit.distance, greenBeam);

        }
         else
        {
            // Draw a line from the enemy's position to the maximum distance of the raycast
            Debug.DrawRay(transform.position, transform.forward * raycastDistance, greenBeam);
        }


    }
}
