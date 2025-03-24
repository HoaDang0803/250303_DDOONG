using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pickUpDistance = 1.5f;

    public GameObject messagePanel;

    public Item item;
    public int count = 1;

    private void Start()
    {
        player = GameManager.instance.player.transform;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (distance < 0.1f)
        {
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.LogWarning("Inventory Container is null");
            }
            Destroy(gameObject);
        }
    }

    public void OpenMessagePanel(string message)
    {
        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }
}
