using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float sprintCooldown = 0.5f;
    public float movementSmoothing = 0.1f; // For smoother movement

    [Header("Input Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private Vector2 movement;
    private bool isSprinting = false;
    private float originalMoveSpeed;
    private bool isSprintingAvailable = true;
    public Animator animator;
    public float currentSpeed { get; private set; }

    private void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (!PlayerStats.Instance.isdead)
        {
            HandleMovementInput();
            HandleSprinting();
            HandleAnimation();
        }
    }

    private void HandleMovementInput()
    {
        movement = Vector2.zero;

        if (Input.GetKey(upKey)) movement.y += 1;
        if (Input.GetKey(downKey)) movement.y -= 1;
        if (Input.GetKey(leftKey)) movement.x -= 1;
        if (Input.GetKey(rightKey)) movement.x += 1;

        movement = movement.normalized; // Prevent faster diagonal movement

        // Calculate currentSpeed (0 when idle, 1 when walking, scaled by sprintMultiplier when sprinting)
        currentSpeed = movement.magnitude * (isSprinting ? sprintMultiplier : 1f);
    }

    private void HandleSprinting()
    {
        if (isSprintingAvailable && Input.GetKey(sprintKey))
        {
            isSprinting = true;
            moveSpeed = originalMoveSpeed * sprintMultiplier;
        }
        else
        {
            isSprinting = false;
            moveSpeed = originalMoveSpeed;
        }

        // Sprint cooldown logic
        if (isSprinting && !isSprintingAvailable)
        {
            StartCoroutine(SprintCooldown());
        }
    }

    private void HandleAnimation()
    {
        // Pass the current speed to the animator to control the animations
        animator.SetFloat("Speed", currentSpeed);
    }

    private IEnumerator SprintCooldown()
    {
        yield return new WaitForSeconds(sprintCooldown);
        isSprintingAvailable = true;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Calculate the target position based on movement input
        Vector2 targetPosition = (Vector2)transform.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Smoothly move towards the target position using Vector2.Lerp
        transform.position = Vector2.Lerp(transform.position, targetPosition, movementSmoothing);

            }
}
