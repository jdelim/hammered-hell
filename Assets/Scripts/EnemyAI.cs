using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// Made by Luis
public class EnemyAI : MonoBehaviour
{
    public bool playerSeen =  false;
    private Transform player; // A reference to the player's transform
    private GameObject playerObject; //refrecnces the whole player object and allows you to call on scripts

    // these are used for detecting the player
    public float raycastDistance = 10f;
    public int raycastCount = 70;
    public float coneAngle = 120f;

    public float circleRayDistance = 10f;
    public int circleRayCount = 180;
    public float circleAngle = 360f;

    // generic colors
    private Color greenBeam = new Color(0, 1, 0);
    private Color redBeam = new Color(1, 0, 0);

    private Color blueBeam = new Color(0, 0, 1);

    //Enemy Values
    private NavMeshAgent agent;

    //patrol variables
    public float length = 10;
    public float width =  10;
    private Vector3[] corners;
    private int currentCornerIndex = 0;

    //Attack variables
    public float attackDistance = 2f; // Distance from player to start attacking

    public float attackDamage = 1f;
    public float attackDelay = 1f; // Delay between attacks
    private float nextAttackTime = 0f; // Time of the next attack

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        
        // Patrol Variables. Do not change anything...

        corners = new Vector3[4];

        corners[0] = new Vector3(transform.position.x + width/2, transform.position.y, transform.position.z + length/2);
        corners[1] = new Vector3(transform.position.x - width/2, transform.position.y, transform.position.z + length/2);
        corners[2] = new Vector3(transform.position.x - width/2, transform.position.y, transform.position.z - length/2);
        corners[3] = new Vector3(transform.position.x + width/2, transform.position.y, transform.position.z - length/2);

        agent.SetDestination(corners[currentCornerIndex]);
        // between these lines

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = GameObject.FindWithTag("Player");
        //only have one Player Object per scene

        }
    void Update()
    {
        
        if(!playerSeen){
            DetectPlayer();
           
            PatrolArea();
        
        }else {
            AttackPlayer();
            FollowPlayer();
        }
        
        
        
    }

    public void PatrolArea(){

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            currentCornerIndex++;

            if (currentCornerIndex >= corners.Length)
            {
                currentCornerIndex = 0;
            }

            agent.SetDestination(corners[currentCornerIndex]);
        }


    }
    public void FollowPlayer()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.SetDestination(player.position);

    }

    public void DetectPlayer(){

        // this controls the raycasts that represent the enemies vision
        for (int i = 0; i < raycastCount; i++)
        {
            float angle = (-coneAngle / 2) + (coneAngle / (raycastCount - 1)) * i; // Calculate the angle between each raycast

            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward; // Calculate the direction of the current raycast

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, raycastDistance))
            {
                // If the raycast hits an object with the "Player" tag, print "Player detected" to the console
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player detected");
                    //Debug.DrawRay(transform.position, direction * hit.distance, redBeam);
                    playerSeen = true;
                }
                else
                {
                    // Draw a line from the enemy's position to the point where the raycast hits an object
                    Debug.DrawRay(transform.position, direction * hit.distance, greenBeam);
                }
            }
            else
            {
                // Draw a line from the enemy's position to the maximum distance of the raycast
                Debug.DrawRay(transform.position, direction * raycastDistance, greenBeam);
            }
        } 

        // for the circle raycast
        for (int j = 0; j < circleRayCount; j++)
        {
            float angle = (-circleAngle / 2) + (circleAngle / (circleRayCount - 1)) * j; // Calculate the angle between each raycast

            Vector3 circleDirection = Quaternion.Euler(0, angle, 0) * transform.forward; // Calculate the direction of the current raycast

            RaycastHit hit;
            if (Physics.Raycast(transform.position, circleDirection, out hit, circleRayDistance))
            {
                // If the raycast hits an object with the "Player" tag, print "Player detected" to the console
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("Player detected");
                    //Debug.DrawRay(transform.position, direction * hit.distance, redBeam);
                    playerSeen = true;
                }
                else
                {
                    // Draw a line from the enemy's position to the point where the raycast hits an object
                    Debug.DrawRay(transform.position, circleDirection * hit.distance, blueBeam);
                }
            }
            else
            {
                // Draw a line from the enemy's position to the maximum distance of the raycast
                Debug.DrawRay(transform.position, circleDirection * circleRayDistance, blueBeam);
            }
        }






    }

    public void AttackPlayer()
{
    if (Vector3.Distance(transform.position, player.position) <= attackDistance)
    {
        agent.isStopped = true; // Stop the agent from moving
        transform.LookAt(player); // Look at the player

        if (Time.time >= nextAttackTime)
        {
            // Attack the player
            //Debug.Log("Attacking player...");
        
            Health playerHealth = playerObject.GetComponent<Health>();
            
            if (playerHealth != null)
            {
                playerHealth.amount -= attackDamage;
            }
            
            nextAttackTime = Time.time + attackDelay;
        }
    }
}

}
