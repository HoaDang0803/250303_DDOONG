using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody rb;
    public int maxHealth = 10;
    public int currentHealth;
    private bool isTakingDamage = false;
    private bool isDie = false;
    [SerializeField] private ItemContainer inventory;


    public Image healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;

        StartCoroutine(RegenerateHealth());
    }

    void Update()
    {
        if (isDie) return;
        
        UpdateHealthBar();

        if (transform.position.y < -5)
        {
            TakeDamage(Random.Range(2, 4));
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            int damage = Random.Range(1, 3);
            TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage");

            // Xác định hướng đẩy ngược
            Vector3 pushDirection = (transform.position - hit.transform.position).normalized;
            float pushDistance = 1.5f;
            float pushForce = 5f;

            // Đẩy Player
            StartCoroutine(PushBack(pushDirection, pushDistance));

            // Đẩy Enemy
            Rigidbody enemyRb = hit.gameObject.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(-pushDirection * pushForce, ForceMode.Impulse);
            }
        }
    }

    IEnumerator PushBack(Vector3 direction, float distance)
    {
        CharacterController controller = GetComponent<CharacterController>();
        float pushTime = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < pushTime)
        {
            controller.Move(direction * (distance / pushTime) * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap") && !isTakingDamage)
        {
            StartCoroutine(DamageOverTime(2f, other));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            isTakingDamage = false;
        }
    }

    IEnumerator DamageOverTime(float interval, Collider trap)
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            TakeDamage(1);
            Debug.Log("Player took damage from trap!");

            yield return new WaitForSeconds(interval);

            if (!isTakingDamage) break;
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    IEnumerator RegenerateHealth()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(5f);
            if (currentHealth < maxHealth)
            {
                Heal(1);
                Debug.Log("Player regenerated 1 health!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
    }

    void Heal(int heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        isDie = true;
        SoundController.instance.PlaySoundEffect(SoundController.instance.die);
        Invoke("Reset", 2f);
    }

    private void Reset()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            inventory.slots[i].item = null;
            inventory.slots[i].count = 0;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
