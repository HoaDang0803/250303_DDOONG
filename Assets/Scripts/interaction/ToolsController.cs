using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    PlayerController player;
    Rigidbody2D rb;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfHitBox = 1.2f;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, sizeOfHitBox);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Item"))
                {
                    if (Vector2.Distance(hitCollider.transform.position, transform.position) < offsetDistance)
                    {
                        hitCollider.GetComponent<ToolHit>().Hit();
                    }
                }
            }
        }
    }

}
