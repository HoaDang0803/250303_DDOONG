using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject sword;
    public bool canAttack = true;
    public float attackCooldown = 1f;
    public bool isAttacking = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        canAttack = false;
        StartCoroutine(ResetAttackCoolDown());
    }

    IEnumerator ResetAttackCoolDown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
