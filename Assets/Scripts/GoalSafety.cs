using UnityEngine;

public class GoalSafety : MonoBehaviour
{
    [SerializeField] private GameObject safetyWalls;

    private void Start()
    {
        if (safetyWalls != null) safetyWalls.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<HoverboardController>() != null)
        {
            if (safetyWalls != null)
            {
                safetyWalls.SetActive(true);
                Debug.Log("Safety Walls Engaged. You are safe on the final platform.");
            }
        }
    }
}
