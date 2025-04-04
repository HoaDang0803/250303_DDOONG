using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject sword;
    public bool canAttack = true;
    public float attackCooldown = 1f;
    public bool isAttacking = false;

    public Animator swordAnimator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            SoundController.instance.PlaySoundEffect(SoundController.instance.attack);
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = swordAnimator.GetComponent<Animator>();
        anim.SetTrigger("Attack");
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
