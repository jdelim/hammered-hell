using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    public GameObject MWeapon;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public AudioClip MWeaponAttackSound;
    public bool IsAttacking;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                MWeaponAttack();
            }
        }
    }

    public void MWeaponAttack()
    {
        IsAttacking = true;
        CanAttack = false;
        Animator anim = MWeapon.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(MWeaponAttackSound); 
        StartCoroutine(ResetAttackCooldown());

    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;

    }
}
