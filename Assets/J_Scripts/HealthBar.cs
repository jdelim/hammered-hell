using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health targetHealth;
    private Image image;
    public bool isPlayer;

    void Awake()
    {
        // this is rly bad code b/c it uses hardcoded int values but it works
        image = GetComponent<Image>();
        if (targetHealth.amount == 100)
        {
            isPlayer = true;
        }
        else if (targetHealth.amount == 300)
        {
            isPlayer = false;
        }
    }

    void Update()
    {
        if (isPlayer)
        {
            image.fillAmount = targetHealth.amount / 100;
        }
        else if (!isPlayer)
        {
            image.fillAmount = targetHealth.amount / 300;
        }
    }
}
