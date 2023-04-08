using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float amount;
    public UnityEvent onDeath;

    void Update(){

        if(amount <= 0 ){
            
            onDeath.Invoke();
            Destroy(gameObject);}
    }
        
    }