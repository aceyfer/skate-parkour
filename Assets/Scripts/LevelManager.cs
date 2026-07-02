using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Configuration")]
    [SerializeField] private List<GameObject> portals;
    [SerializeField] private int unlockedLevel = 1;
    [SerializeField] private string currentSubLevel = "1A";
    [SerializeField] private GameObject level1BRoot;
    [SerializeField] private GameObject level1CRoot;
    [SerializeField] private GameObject level1DRoot;
    [SerializeField] private TMPro.TextMeshProUGUI fragmentCounterText;
[SerializeField] private AudioClip unlockSound;
    [SerializeField] private AudioSource globalSource;
    private int fragmentsCollected = 0;
private bool goalMet = false;
    private Vector3 currentCheckpointPosition;
    private Quaternion currentCheckpointRotation = Quaternion.identity;
    private bool hasCheckpoint = false;

    [Header("Teleport Destinations")]
public Vector3 level1B_Start = new Vector3(-30, 2, 0);
    public Vector3 level1C_Start = new Vector3(30, 2, 0);
    public Vector3 level1D_Start = new Vector3(0, 2, 30);
    public Vector3 level2_Start = new Vector3(0, 10, -100);

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Initially hide all portals
        foreach (var p in portals)
        {
            if (p != null) p.SetActive(false);
        }
    }

    public void CompleteLevel()
    {
        Debug.Log("Level " + unlockedLevel + " Completed!");
        
        // Enable portal for current level (assuming portal index matches level-1)
        int currentLevelIndex = unlockedLevel - 1;
        if (currentLevelIndex >= 0 && currentLevelIndex < portals.Count)
        {
            if (portals[currentLevelIndex] != null)
            {
                portals[currentLevelIndex].SetActive(true);
            }
        }

        unlockedLevel++;
    }

    public void CompleteSubLevel(string subLevel)
    {
        if (goalMet) 
        {
            Debug.Log("Sub-Level completion ignored: Goal already met.");
            return;
        }

        Debug.Log("Sub-Level " + subLevel + " Completed! Activating next zone...");
        goalMet = true;
        fragmentsCollected++;
        UpdateHUD();
        
        string nextSub = "DONE";
        if (subLevel == "1A") 
        {
            nextSub = "1B";
            if (level1BRoot != null) 
            {
                level1BRoot.SetActive(true);
                Debug.Log("Activated Level1B_Root.");
            }
            else Debug.LogError("Level1B_Root reference is missing in LevelManager!");
        }
        else if (subLevel == "1B")
        {
            nextSub = "1C";
            if (level1CRoot != null) 
            {
                level1CRoot.SetActive(true);
                Debug.Log("Activated Level1C_Root.");
            }
            else Debug.LogError("Level1CRoot reference is missing in LevelManager!");
        }
        else if (subLevel == "1C")
        {
            nextSub = "1D";
            if (level1DRoot != null) 
            {
                level1DRoot.SetActive(true);
                Debug.Log("Activated Level1D_Root.");
            }
            else Debug.LogError("Level1DRoot reference is missing in LevelManager!");
        }
        else if (subLevel == "1D")
        {
            nextSub = "Level 2";
        }

        if (ChoicePopup.Instance != null)
        {
            ChoicePopup.Instance.Show("Level " + subLevel + " Completed! Next: " + nextSub + ". Land on platform to teleport!");
            if (globalSource != null && unlockSound != null) globalSource.PlayOneShot(unlockSound);
        }
        else Debug.LogWarning("ChoicePopup Instance not found.");
    }

    private void UpdateHUD()
    {
        if (fragmentCounterText != null)
        {
            fragmentCounterText.text = "Fragments: " + fragmentsCollected;
        }
    }

    private void Start()
    {
        AlignPlayerToCurrentSubLevelStart();
        InitializeCheckpoint();
        UpdateHUD();
    }

    public bool IsGoalMet() => goalMet;

    public void TeleportToNext(string subLevelOverride = "")
    {
        if (!goalMet) return;

        Vector3 dest = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        string subLevel = string.IsNullOrEmpty(subLevelOverride) ? currentSubLevel : subLevelOverride;

        if (subLevel == "1A")
        {
            dest = level1B_Start;
            rot = Quaternion.Euler(0, -90, 0); // West
            currentSubLevel = "1B";
        }
        else if (subLevel == "1B")
        {
            dest = level1C_Start;
            rot = Quaternion.Euler(0, 90, 0); // East
            currentSubLevel = "1C";
        }
        else if (subLevel == "1C")
        {
            dest = level1D_Start;
            rot = Quaternion.Euler(0, 0, 0); // North
            currentSubLevel = "1D";
        }
        else if (subLevel == "1D")
        {
            dest = level2_Start;
            rot = Quaternion.identity;
            currentSubLevel = "DONE";
            CompleteLevel();
        }

        goalMet = false; // Reset for next sub-level
        TeleportPlayer(dest, rot);
    }

    private void TeleportPlayer(Vector3 destination, Quaternion rotation)
{
        GameObject player = GameObject.Find("Player_Hoverboard");
        if (player == null) player = GameObject.Find("Player");
        
        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            
            // Safety: offset destination slightly up to avoid spawning inside ground
            player.transform.position = destination + Vector3.up * 0.2f;
            player.transform.rotation = rotation;
            SetCheckpoint(player.transform.position, player.transform.rotation);
            
            if (cc != null) cc.enabled = true;

            var hover = player.GetComponent<HoverboardController>();
            if (hover != null) hover.ResetVelocity();
            
            var skate = player.GetComponent<SkateParkourController>();
            if (skate != null) skate.ResetVelocity();
        }
    }

    public string GetCurrentSubLevel() => currentSubLevel;

    private void AlignPlayerToCurrentSubLevelStart()
    {
        if (currentSubLevel != "1A") return;

        GameObject player = GameObject.Find("Player_Hoverboard");
        if (player == null) player = GameObject.Find("Player");

        if (player != null)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void InitializeCheckpoint()
    {
        GameObject player = GameObject.Find("Player_Hoverboard");
        if (player == null) player = GameObject.Find("Player");

        if (player != null)
        {
            SetCheckpoint(player.transform.position, player.transform.rotation);
        }
    }

    public void SetCheckpoint(Vector3 position, Quaternion rotation)
    {
        currentCheckpointPosition = position;
        currentCheckpointRotation = rotation;
        hasCheckpoint = true;
    }

    public bool TryGetCheckpoint(out Vector3 position, out Quaternion rotation)
    {
        position = currentCheckpointPosition;
        rotation = currentCheckpointRotation;
        return hasCheckpoint;
    }
}
