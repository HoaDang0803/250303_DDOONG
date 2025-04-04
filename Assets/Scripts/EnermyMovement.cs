using UnityEngine;

public class EnermyMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 5f; // Tốc độ xoay

    private void Update()
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
}
