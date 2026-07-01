using UnityEngine;
using UnityEngine.InputSystem;

public class SkateOrbitCamera : MonoBehaviour
{
    [Header("Targeting")]
    [Tooltip("The player capsule the camera will orbit.")]
    public Transform target;
    [Tooltip("Offset to focus on the player's upper body instead of their feet.")]
    public Vector3 targetOffset = new Vector3(0, 1.5f, 0);

    [Header("Distance Settings")]
    [Tooltip("How far the camera stays from the player.")]
    public float distance = 7.0f;
    [Tooltip("Minimum distance for zooming (if implemented).")]
    public float minDistance = 2.0f;
    [Tooltip("Maximum distance for zooming (if implemented).")]
    public float maxDistance = 15.0f;

    [Header("Rotation Settings")]
    [Tooltip("Speed of horizontal and vertical rotation.")]
    public float sensitivity = 0.2f;
    [Tooltip("Limit how far down the camera can look.")]
    public float minVerticalAngle = -20f;
    [Tooltip("Limit how far up the camera can look.")]
    public float maxVerticalAngle = 80f;

    // Current rotation state
    private float rotationX = 0f; // Horizontal (Yaw)
    private float rotationY = 20f; // Vertical (Pitch)

    private InputAction lookAction;

    private void Start()
    {
        // Cache the 'Look' action from project-wide input settings
        lookAction = InputSystem.actions.FindAction("Look");

        // Initialize rotation to match the camera's starting orientation if possible
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.y;
        rotationY = angles.x;

        // Lock cursor for a better orbit experience (optional but recommended for mouse)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null || lookAction == null) return;

        // 1. READ INPUT
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // 2. UPDATE ROTATION ANGLES
        // Mouse Delta is usually much larger than Stick Delta, 
        // but we'll use a unified sensitivity for simplicity here.
        if (lookInput.sqrMagnitude > 0.01f)
        {
            rotationX += lookInput.x * sensitivity;
            rotationY -= lookInput.y * sensitivity;

            // 3. CLAMP VERTICAL ROTATION
            // Prevents the camera from flipping upside down
            rotationY = Mathf.Clamp(rotationY, minVerticalAngle, maxVerticalAngle);
        }

        // 4. CALCULATE FINAL POSITION AND ROTATION
        // Convert Euler angles to a Quaternion
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // Position is: Player Center - (Forward Direction * Distance)
        Vector3 focusPosition = target.position + targetOffset;
        Vector3 position = focusPosition - (rotation * Vector3.forward * distance);

        // 5. APPLY TO TRANSFORM
        transform.rotation = rotation;
        transform.position = position;
    }

    // Optional: Allow unlocking cursor if user presses Escape
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
