using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("The player object the camera will follow.")]
    public Transform target;
    
    [Tooltip("The position offset relative to the player (X: Side, Y: Height, Z: Distance).")]
    public Vector3 offset = new Vector3(0, 3f, -7f);
    
    [Tooltip("How smoothly the camera catches up to the player. Lower is smoother.")]
    [Range(0.01f, 1.0f)]
    public float smoothSpeed = 0.125f;

    [Header("Look Settings")]
    [Tooltip("Height offset for where the camera looks (usually the player's head/shoulders).")]
    public float lookHeightOffset = 1.5f;

    // Internal velocity used by SmoothDamp
    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate the target position relative to the player's rotation
        // This ensures the camera stays BEHIND the player as they turn.
        Vector3 desiredPosition = target.position + (target.rotation * offset);

        // 2. Smoothly interpolate from current position to the desired position
        // We use SmoothDamp for a high-quality, non-jittery follow.
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // 3. Always look at the player
        // We look slightly above the player's base so they are centered in the frame.
        Vector3 lookTarget = target.position + Vector3.up * lookHeightOffset;
        transform.LookAt(lookTarget);
    }
}
