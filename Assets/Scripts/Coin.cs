using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float vacuumRange = 10f;
    [SerializeField] private float vacuumSpeed = 8f;
    [SerializeField] private AudioClip collectSound;
    public bool isGoalCoin = false;
    public string belongsToSubLevel = ""; // e.g. "1A", "1B"

    private Vector3 startPos;
    private Transform player;

    private void Start()
    {
        startPos = transform.position;
        GameObject playerObj = GameObject.Find("Player_Hoverboard");
        if (playerObj != null) player = playerObj.transform;
        
        // Unparent to avoid platform scale stretching
        transform.SetParent(null);
        transform.localScale = Vector3.one;

        // Ensure there is a trigger collider
        var collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<SphereCollider>();
        }
        collider.isTrigger = true;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        
        if (player != null)
        {
            float dist = Vector3.Distance(transform.position, player.position);
            if (dist < vacuumRange)
            {
                // Vacuum effect
                transform.position = Vector3.MoveTowards(transform.position, player.position + Vector3.up * 0.5f, vacuumSpeed * Time.deltaTime);
                return;
            }
        }

        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<HoverboardController>() != null || other.GetComponent<SkateParkourController>() != null)
        {
            // Collect coin
            Debug.Log("Coin collected!");
            if (collectSound != null) AudioSource.PlayClipAtPoint(collectSound, transform.position);
            
            if (isGoalCoin)
            {
                string current = belongsToSubLevel;
                if (string.IsNullOrEmpty(current)) current = LevelManager.Instance.GetCurrentSubLevel();
                
                LevelManager.Instance.CompleteSubLevel(current);
            }

            gameObject.SetActive(false);
        }
    }
}
