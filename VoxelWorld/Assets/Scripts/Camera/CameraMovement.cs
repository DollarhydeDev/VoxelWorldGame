using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 50f;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private bool lockCursor = true;

    private float maxVerticalLook = 90.0f;

    private float verticalRotation = 0f;
    private PlayerInputEvents playerInputEvents;

    void Awake()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        playerInputEvents = new PlayerInputEvents();
        MapEvents();
    }

    void LateUpdate()
    {
        if (followTarget != null)
        {
            MoveCamera();
        }
    }

    public void SetFollowTarget(Transform target)
    {
        followTarget = target;
    }
    public void SetCameraSettings(float mouseSensitivity, float maxVerticalLook)
    {
        this.mouseSensitivity = mouseSensitivity;
        this.maxVerticalLook = maxVerticalLook;
    }

    private void MoveCamera()
    {
        // Follow the target with an offset.
        transform.position = followTarget.position + cameraOffset;

        // Quaternions are breaking my brain :')

        // Set the y rotation of the camera to the rotation of the targets y axis.
        Vector3 currentRotation = new Vector3(0, followTarget.eulerAngles.y, 0);

        // Then, account for the vertical rotation.
        currentRotation.x = -verticalRotation;

        // Finally, apply the rotation to the camera.
        transform.eulerAngles = currentRotation;
    }

    private void MapEvents()
    {
        playerInputEvents.Player.Look.performed += ctx =>
        {
            // Grab the mouse input
            Vector2 mouseInput = ctx.ReadValue<Vector2>();

            // Apply the input to the vertical rotation
            verticalRotation += mouseInput.y * mouseSensitivity * Time.deltaTime;

            // Then clamp the vertical rotation
            verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalLook, maxVerticalLook);
        };
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
