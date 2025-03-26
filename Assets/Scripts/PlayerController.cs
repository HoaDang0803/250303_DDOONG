using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.75f;

    public int numberOfJumps = 0;
    public int maxNumberOfJumps = 2;
    private Vector3 velocity;
    private bool isGrounded;

    private float dirX = 0f;
    private float dirZ = 0f;

    public Animator anim;
    private enum MovementState { Idle, Running, Jumping, Faling, Looting, Attacking }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            numberOfJumps = 0;
        }
        CharacterMove();
        Jump();
        Run();
        UpdateAnimationState();
    }

    public float rotationSmoothTime = 0.1f; // Thời gian làm mượt xoay
    private float currentAngleVelocity; // Biến lưu tốc độ xoay

    public void CharacterMove()
    {
        dirX = Input.GetAxis("Vertical");
        dirZ = -Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(dirX, 0, dirZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7f;
        }
        else
        {
            speed = 2.5f;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && numberOfJumps < maxNumberOfJumps)
        {
            numberOfJumps++;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (!isGrounded)
        {
            if (velocity.y > 0.1f)
                state = MovementState.Jumping;
            else
                state = MovementState.Faling;
        }
        else if (dirX != 0f || dirZ != 0f)
        {
            state = MovementState.Running;
        }
        else
        {
            state = MovementState.Idle;
        }
        anim.SetInteger("state", (int)state);
    }
}
