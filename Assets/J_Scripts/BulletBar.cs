using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletBar : MonoBehaviour
{
    private TMP_Text text;
    //public GameObject targetPlayer;
    public GunController pistol;
    public GunController rifle;
    public ShotgunController shotty;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = shotty.bullets + " / " + shotty.maxAmmo;
        text.text = pistol.bullets + " / " + pistol.maxAmmo;
        text.text = rifle.bullets + " / " + rifle.maxAmmo;
    }
}
