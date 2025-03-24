using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    private bool isTakingDamage = false; // Kiểm soát việc trừ máu theo thời gian

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
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
            float pushDistance = 1.5f; // Điều chỉnh khoảng cách đẩy
            float pushForce = 5f; // Điều chỉnh lực đẩy cho Enemy

            // Đẩy Player bằng CharacterController.Move()
            StartCoroutine(PushBack(pushDirection, pushDistance));

            // Đẩy Enemy bằng Rigidbody.AddForce()
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
        float pushTime = 0.2f; // Thời gian đẩy lùi (giảm nếu muốn nhanh hơn)
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
        if (other.gameObject.CompareTag("Health"))
        {
            int heal = Random.Range(0, 5);
            Debug.Log("Player healed " + heal + " health");
            Heal(heal);
        }

        // Khi nhân vật bước vào khu vực chông/mìn
        if (other.gameObject.CompareTag("Trap") && !isTakingDamage)
        {
            StartCoroutine(DamageOverTime(2f, other)); // Gây sát thương mỗi 2 giây
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Khi nhân vật rời khỏi khu vực chông/mìn, dừng trừ máu
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

            // Nếu nhân vật rời khỏi bẫy thì dừng coroutine
            if (!isTakingDamage) break;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current Health: " + currentHealth);
    }

    void Heal(int heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }

    void Die()
    {
        Debug.Log("Player died");
        Time.timeScale = 0;
    }
}
