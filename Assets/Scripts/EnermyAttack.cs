using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Phạm vi tấn công
    public float attackRate = 1.5f; // Thời gian giữa các đòn đánh
    public int attackDamage = 3; // Sát thương gây ra

    private Transform player;
    private float nextAttackTime = 1f;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy attacks Player!");

        // Kích hoạt Animation tấn công
        animator.SetTrigger("Attack");

        // Gây sát thương sau 0.5s (giả sử thời gian ra đòn)
        Invoke(nameof(DealDamage), 0.25f);
    }

    private void DealDamage()
    {
        if (player != null)
        {
            player.GetComponent<PlayerLife>()?.TakeDamage(attackDamage);
        }
    }
}
