using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pickUpDistance = 1.5f;

    public GameObject messagePanel;
    public TextMeshProUGUI mesageText;

    public Item item;
    public int count = 1;
    private bool isCollecting = false;
    private bool isNearItem = false;
    private void Start()
    {
        player = GameManager.instance.player.transform;
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Nếu trong phạm vi thì hiện messagePanel, ngược lại thì ẩn
        if (distance <= pickUpDistance)
        {
            if (!isNearItem) // Chỉ bật panel nếu nó chưa bật
            {
                OpenMessagePanel("Nhấn F để nhặt vật phẩm");
                isNearItem = true;
            }

            // Chỉ cho phép nhấn F khi đang trong phạm vi
            if (Input.GetKeyDown(KeyCode.F))
            {
                isCollecting = true;
            }
        }
        else
        {
            if (isNearItem) // Chỉ tắt panel nếu nó đang bật
            {
                CloseMessagePanel();
                isNearItem = false;
            }
            isCollecting = false;
        }

        if (!isCollecting) return;

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
            isCollecting = false;
            CloseMessagePanel();
        }
    }

    public void OpenMessagePanel(string message)
    {
        mesageText.text = message;
        Debug.Log("Open message panel");
        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        mesageText.text = string.Empty;
        Debug.Log("Close message panel");
        messagePanel.SetActive(false);
    }
}
