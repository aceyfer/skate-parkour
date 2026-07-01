using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Header("Teleport Settings")]
    [Tooltip("Where the player will be moved to upon entering the zone.")]
    public Vector3 respawnPosition = new Vector3(0, 2f, 0);

    [Tooltip("Optional: If the player has a specific tag, we can filter for it.")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object entering is the player
        // We look for the CharacterController first since that's what handles movement
        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            // Optional: Further verify it's the player using tag or name
            if (other.CompareTag(playerTag) || other.name.Contains("Player"))
            {
                TeleportPlayer(controller);
            }
        }
    }

    private void TeleportPlayer(CharacterController controller)
    {
        controller.enabled = false;
        controller.transform.position = respawnPosition;
        controller.enabled = true;

        // Reset skater velocity so they don't keep moving after respawn
        var skate = controller.GetComponent<SkateParkourController>();
        if (skate != null) skate.ResetVelocity();
        
        var hover = controller.GetComponent<HoverboardController>();
        if (hover != null) hover.ResetVelocity();

        Debug.Log("Player fell into KillZone! Teleported to: " + respawnPosition);
    }
}
