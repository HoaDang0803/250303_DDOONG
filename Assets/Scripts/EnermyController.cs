using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    private Animator animator;
    private Renderer enemyRenderer;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyRenderer = GetComponent<Renderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Enemy died!");
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
