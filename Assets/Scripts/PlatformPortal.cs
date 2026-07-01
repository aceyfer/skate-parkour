using UnityEngine;

public class PlatformPortal : MonoBehaviour
{
    public string portalSubLevel = ""; // e.g. "1A", "1B"

    private void OnTriggerEnter(Collider other)
    {
        TryTeleport(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TryTeleport(other);
    }

    private void TryTeleport(Collider other)
    {
        // Robust check: check object or parent for any player-related component or tag
        bool isPlayer = other.CompareTag("Player") || 
                        other.GetComponentInParent<HoverboardController>() != null || 
                        other.GetComponentInParent<SkateParkourController>() != null;

        if (isPlayer)
        {
            if (LevelManager.Instance != null && LevelManager.Instance.IsGoalMet())
            {
                Debug.Log("Goal met! Teleporting via platform trigger: " + portalSubLevel);
                LevelManager.Instance.TeleportToNext(portalSubLevel);
            }
            else if (LevelManager.Instance != null && !LevelManager.Instance.IsGoalMet())
            {
                // Optional: log why it failed to help debugging
                // Debug.Log("Player touched portal but Goal is not yet met for: " + portalSubLevel);
            }
        }
    }
}
