using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float horizontalRotationSpeed = 50f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravityStrength = 9.8f;

    [Header("Camera Settings")]
    [SerializeField] private float maxVerticalLook = 90.0f;
    [SerializeField] private float verticalRotationSpeed = 50f;

    // Direction the player is moving in.
    private Vector3 moveDirection;
    private Vector2 lookRotation;

    private PlayerInputEvents playerInputEvents;
    private CharacterController characterController;

    private CameraMovement cameraMovement;

    private void Awake()
    {
        playerInputEvents = new PlayerInputEvents();
        characterController = GetComponent<CharacterController>();
        cameraMovement = FindFirstObjectByType<CameraMovement>();

        cameraMovement.SetFollowTarget(transform);
        cameraMovement.SetCameraSettings(verticalRotationSpeed, maxVerticalLook);

        MapEvents();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        // Apply gravity if needed.
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravityStrength * Time.deltaTime;
        }
        else if (moveDirection.y < 0)
        {
            moveDirection.y = 0;
        }

        // Calculate the direction the player should move in.
        Vector3 worldDirection = transform.rotation * moveDirection;

        // Move the player using the CharacterController.
        characterController.Move(worldDirection * moveSpeed * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        // Rotate the player based on the input values.
        transform.Rotate(Vector3.up * lookRotation.x * horizontalRotationSpeed * Time.deltaTime);
    }

    private void PlayerJump()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y += jumpForce;
        }
        else
        {
            return;
        }
    }
    private void MapEvents()
    {
        // Map events to our input values
        playerInputEvents.Player.Move.performed += ctx => moveDirection = new Vector3(ctx.ReadValue<Vector2>().x, moveDirection.y, ctx.ReadValue<Vector2>().y);
        playerInputEvents.Player.Move.canceled += ctx => moveDirection = new Vector3(0, moveDirection.y, 0);

        playerInputEvents.Player.Jump.performed += ctx => PlayerJump();

        playerInputEvents.Player.Look.performed += ctx => lookRotation = ctx.ReadValue<Vector2>();
        playerInputEvents.Player.Look.canceled += ctx => lookRotation = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInputEvents.Enable();
    }
    private void OnDisable()
    {
        playerInputEvents.Disable();
    }
}