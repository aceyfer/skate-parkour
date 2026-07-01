using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Vector3 destination = new Vector3(5, 3, 0); // Starting Zone

    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null && (other.CompareTag("Player") || other.name == "Player"))
        {
            controller.enabled = false;
            other.transform.position = destination;
            controller.enabled = true;

            SkateParkourController skater = other.GetComponent<SkateParkourController>();
            if (skater != null) skater.ResetVelocity();

            Debug.Log("Portal used! Teleported to Start.");
        }
    }
}
