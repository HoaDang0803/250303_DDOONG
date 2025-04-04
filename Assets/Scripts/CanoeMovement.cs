using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CanoeMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private ItemContainer itemContainer;
    [SerializeField] private string requiredItemName = "Paddle";
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject river;
    public SceneTransition sceneTransition;
    private int currentWaypointIndex = 0;
    private bool canMove = false;
    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear)
        {
            if (!river.activeInHierarchy)
            {
                ShowMessage("Dòng sông đã bị đóng băng! Bạn không thể di chuyển.");
                return;
            }

            if (!HasRequiredItem())
            {
                ShowMessage("Bạn cần một mái chèo để di chuyển bè!");
            }
            else
            {
                ShowMessage("Nhấn E để bắt đầu di chuyển bè.");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    sceneTransition.ChangeScene();
                    canMove = true;
                    HideMessage();
                }
            }
        }
        else
        {
            HideMessage();
        }

        if (canMove)
        {
            MoveCanoe();
        }
    }

    private void MoveCanoe()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;
        targetPosition.y = transform.position.y; // Giữ nguyên chiều cao để không bị nghiêng

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // Di chuyển
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        RotateTowards(targetPosition);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private bool HasRequiredItem()
    {
        return itemContainer.slots.Exists(slot => slot.item != null && slot.item.itemName == requiredItemName);
    }

    private void ShowMessage(string message)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
    }

    private void HideMessage()
    {
        messagePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            other.gameObject.transform.SetParent(transform);
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = true;
            other.gameObject.transform.SetParent(null);
            isPlayerNear = false;
            HideMessage();
        }
    }
}
