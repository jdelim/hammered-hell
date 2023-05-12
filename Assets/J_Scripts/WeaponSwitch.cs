using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* Cassandra */
// This Script must be attached to the Player Object.
// It handles weapon collection & switching weapons by scrolling the middle mouse button.
// It requires 3 canvas child objects (lines 13-15), drag & drop them in the inspector panel.

public class WeaponSwitch : MonoBehaviour
{
    public GameObject shotGunCrosshair;
    public GameObject garandCrosshair;
    public GameObject revolverCrosshair;

    public GameObject Axe;
    public GameObject Shotgun;
    public GameObject Revolver;
    public GameObject Garand;


    public Weapons.Weapon CurrentWeapon;
    private int WeaponNumber = 0;
    public List<Weapons.Weapon> weaponList;
    public UnityEvent onObtainedWeapon; //event

    // Start is called before the first frame update
    void Awake()
    {
        weaponList = new List<Weapons.Weapon>();
        weaponList.Add(Weapons.Weapon.Revolver);
        weaponList.Add(Weapons.Weapon.Knife);
        weaponList.Add(Weapons.Weapon.Garand);
        weaponList.Add(Weapons.Weapon.Shotgun);
        CurrentWeapon = weaponList[0];
    }

    public void AddWeapon(Weapons.Weapon weapon){
        weaponList.Add(weapon);
        Debug.Log("Added "+ weapon);
   }

    void Update()
    {
        // Get Input From The Mouse Wheel
        // if mouse wheel gives a positive value add 1 to WeaponNumber
        // if it gives a negative value decrease WeaponNumber by 1
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            if(WeaponNumber < weaponList.Count-1){
                WeaponNumber = (WeaponNumber + 1);
                CurrentWeapon = weaponList[WeaponNumber];
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            if(WeaponNumber >= 1){
                WeaponNumber = (WeaponNumber - 1);
                CurrentWeapon = weaponList[WeaponNumber];
            }
        }

        if(CurrentWeapon == Weapons.Weapon.Revolver){
            revolverCrosshair.SetActive(true);
            Revolver.SetActive(true);
        }
        else{
            revolverCrosshair.SetActive(false);
            Revolver.SetActive(false);
        }
        if(CurrentWeapon == Weapons.Weapon.Shotgun){
            shotGunCrosshair.SetActive(true);
            Shotgun.SetActive(true);
        }
        else{
            shotGunCrosshair.SetActive(false);
            Shotgun.SetActive(false);
        }
        if(CurrentWeapon == Weapons.Weapon.Garand){
            garandCrosshair.SetActive(true);
            Garand.SetActive(true);
        }
        else{
            garandCrosshair.SetActive(false);
            Garand.SetActive(false);
        }
        if(CurrentWeapon == Weapons.Weapon.Axe){
            Axe.SetActive(true);
        }
        else{
            Axe.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider collider){
    // Check if Player collided with a weapon object
    Weapons weapon = collider.GetComponent<Weapons>();
    if (weapon != null) {
        AddWeapon(weapon.getWeaponType());
        Destroy(weapon.gameObject, 1);
    }
   }
}
