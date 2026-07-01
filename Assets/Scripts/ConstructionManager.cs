using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ConstructionManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private AlienSkateAI alienAI;
    [SerializeField] private List<GameObject> propPrefabs;
    [SerializeField] private LayerMask groundLayer;

    private int selectedPropIndex = 0;
    private GameObject ghostProp;
    private bool buildModeActive = false;
    private Vector3 lastClickPos;
    private bool waitingForAlien = false;

    [SerializeField] private MovementRecorder movementRecorder;

    private void Start()
    {
        if (alienAI == null) alienAI = FindObjectOfType<AlienSkateAI>();
        if (movementRecorder == null) movementRecorder = FindObjectOfType<MovementRecorder>();
    }

    private void Update()
    {
        // Toggle Build Mode (B key)
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            buildModeActive = !buildModeActive;
            if (!buildModeActive && ghostProp != null) 
                ghostProp.SetActive(false);
            
            Debug.Log("Build Mode: " + buildModeActive);
        }

        // Handle Recording (R key)
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (!movementRecorder.IsRecording)
            {
                movementRecorder.StartRecording();
            }
            else
            {
                movementRecorder.StopRecording();
                // Command Alien to replay what was just recorded
                alienAI.StartReplay(movementRecorder.RecordedFrames);
            }
        }

        if (buildModeActive)
{
            HandleGhostPreview();

            if (Mouse.current.leftButton.wasPressedThisFrame && !waitingForAlien)
            {
                CommandAlienToBuild();
            }
        }

        if (waitingForAlien)
        {
            if (alienAI.IsAtDestination())
            {
                SpawnProp(lastClickPos);
                waitingForAlien = false;
            }
        }
    }

    private void HandleGhostPreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            if (ghostProp == null)
            {
                ghostProp = Instantiate(propPrefabs[selectedPropIndex]);
                // Remove colliders from ghost
                foreach (var c in ghostProp.GetComponentsInChildren<Collider>()) c.enabled = false;
                // Add transparency or different material? For now just the prop.
            }

            ghostProp.SetActive(true);
            ghostProp.transform.position = hit.point;
            ghostProp.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
            ghostProp.transform.eulerAngles = new Vector3(0, ghostProp.transform.eulerAngles.y, 0);
        }
        else if (ghostProp != null)
        {
            ghostProp.SetActive(false);
        }
    }

    private void CommandAlienToBuild()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            lastClickPos = hit.point;
            alienAI.StopReplay(); // Stop any replay when building
            alienAI.SetDestination(hit.point);
            waitingForAlien = true;
            Debug.Log("Alien commanded to build at " + hit.point);
        }
    }

    private void SpawnProp(Vector3 position)
    {
        var prop = Instantiate(propPrefabs[selectedPropIndex], position, ghostProp.transform.rotation);
        // Ensure NavMesh updates if using carving
        // Or just let it be.
        Debug.Log("Prop spawned by Alien!");
    }
}
