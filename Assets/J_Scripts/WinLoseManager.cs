using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Cassandra */
// This script must be attatched to empty WinLoseManager object. Uncomment lines 30 & 36 once we have the scenes. 
// Drag & Drop the Player object in the espector panel. 
// If you are working on Level 4: Uncomment lines 14, 19, 25, & 37 --> Drag & Drop Boss enemy object in the insepctor

public class WinLoseManager : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    [SerializeField] Health bossEnemyHealth;

    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        bossEnemyHealth = GameObject.Find("Boss").GetComponent<Health>();
    }

    void Awake()
    {
        playerHealth.onDeath.AddListener(OnPlayerDied);
        bossEnemyHealth.onDeath.AddListener(OnBossEnemyDied);
    }

    void OnPlayerDied()
    {
        //SceneManager.LoadScene("LoseScreen");
        Debug.Log("Player Died");
    }

    void OnBossEnemyDied()
    {
        //SceneManager.LoadScene("WinScreen");
        Debug.Log("Player Won");
    }
}
