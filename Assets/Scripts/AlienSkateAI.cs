using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterController))]
public class AlienSkateAI : MonoBehaviour
{
    [Header("Skate Physics (Matches Player)")]
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float maxSpeed = 18f;
    [SerializeField] private float turnSpeed = 250f;
    [SerializeField] private float grip = 10f;
    [SerializeField] private float gravity = -18f;
    [SerializeField] private float friction = 0.35f;

    private NavMeshAgent agent;
    private CharacterController controller;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    private void Awake()
    {
        EnsureComponents();
    }

    private void EnsureComponents()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (controller == null) controller = GetComponent<CharacterController>();
    }

    [Header("Freestyle Behavior")]
[SerializeField] private bool allowFreestyle = true;
    [SerializeField] private float roamRadius = 30f;
    [SerializeField] private float waitAtPointTime = 2f;
    private float roamTimer = 0f;
    private bool isWaiting = false;

    [Header("Replay System")]
    private List<MovementRecorder.MovementFrame> replayFrames;
    private int currentReplayIndex = -1;
    private bool isReplaying = false;

    [Header("Testing Behavior")]
    [SerializeField] private List<Transform> testTargets;
    public int CurrentTestIndex => currentTestIndex;
    public bool IsTesting => isTesting;
    private int currentTestIndex = 0;
    private bool isTesting = false;

    private void Update()
    {
        // 1. Sync agent position to character position
        agent.nextPosition = transform.position;

        // 2. Handle State logic
        Vector3 desiredDir = Vector3.zero;

        if (isReplaying)
        {
            HandleReplay();
        }
        else if (isTesting)
        {
            HandleTesting();
            desiredDir = agent.desiredVelocity.normalized;
        }
        else if (agent.hasPath)
        {
            desiredDir = agent.desiredVelocity.normalized;
            if (agent.remainingDistance < 0.5f)
            {
                desiredDir = Vector3.zero;
                if (!isWaiting) StartCoroutine(WaitAndRoam());
            }
        }
        else if (allowFreestyle && !isWaiting)
        {
            PickRandomRoamPoint();
        }

        if (!isReplaying)
        {
            HandleMovement(desiredDir);
            CheckForGaps();
        }

        // 3. Apply final move
        Vector3 finalMove = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(finalMove * Time.deltaTime);
    }

    private void CheckForGaps()
    {
        if (!controller.isGrounded || isReplaying) return;

        // Raycast ahead to see if we are about to fall off
        // We look further ahead based on speed
        float lookAhead = Mathf.Max(1.5f, horizontalVelocity.magnitude * 0.2f);
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f + transform.forward * lookAhead, Vector3.down);
        
        Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.red);

        if (!Physics.Raycast(ray, out RaycastHit hit, 2.0f))
        {
            // No ground ahead! Jump!
            // Only jump if we have some forward momentum
            if (horizontalVelocity.magnitude > 3f) 
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        if (verticalVelocity > 0) return; // Already jumping
        
        // Calculate jump force based on gravity to hit a target height (e.g. 4 units)
        float jumpHeight = 4.0f;
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        Debug.Log("[QA] Alien AI: Jumping gap at " + transform.position);
    }

    public void StartTest(List<Transform> targets)
    {
        testTargets = targets;
        currentTestIndex = 0;
        isTesting = true;
        isReplaying = false;
        if (testTargets.Count > 0) agent.SetDestination(testTargets[currentTestIndex].position);
    }

    private void HandleTesting()
    {
        if (testTargets == null || testTargets.Count == 0) return;

        if (IsAtDestination())
        {
            Debug.Log("Alien AI: Obstacle " + currentTestIndex + " tested successfully.");
            currentTestIndex++;
            if (currentTestIndex >= testTargets.Count)
            {
                isTesting = false;
                Debug.Log("Alien AI: All obstacles tested!");
            }
            else
            {
                agent.SetDestination(testTargets[currentTestIndex].position);
            }
        }
    }

    public void StartReplay(List<MovementRecorder.MovementFrame> frames)
    {
        if (frames == null || frames.Count == 0) return;
        replayFrames = frames;
        currentReplayIndex = 0;
        isReplaying = true;
        agent.isStopped = true;
        agent.ResetPath();
        Debug.Log("Alien AI: Starting replay of recorded movements.");
    }

    public void StopReplay()
    {
        isReplaying = false;
        currentReplayIndex = -1;
        agent.isStopped = false;
    }

    private void HandleReplay()
    {
        if (currentReplayIndex < 0 || currentReplayIndex >= replayFrames.Count)
        {
            StopReplay();
            return;
        }

        MovementRecorder.MovementFrame frame = replayFrames[currentReplayIndex];
        
        // Move towards frame position
        Vector3 targetPos = frame.position;
        Vector3 moveDir = (targetPos - transform.position);
        
        if (moveDir.magnitude > 0.05f)
        {
            controller.Move(moveDir);
        }
        
        transform.rotation = frame.rotation;
        
        currentReplayIndex++;
    }

    private void PickRandomRoamPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    private System.Collections.IEnumerator WaitAndRoam()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitAtPointTime);
        isWaiting = false;
        if (allowFreestyle && !agent.hasPath) PickRandomRoamPoint();
    }

    private void HandleMovement(Vector3 desiredDir)
    {
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0) verticalVelocity = -2f;

            if (desiredDir != Vector3.zero)
            {
                // --- STEERING ---
                float angle = Vector3.SignedAngle(transform.forward, desiredDir, Vector3.up);
                float rotation = Mathf.Clamp(angle, -1f, 1f) * turnSpeed * Time.deltaTime;
                transform.Rotate(0, rotation, 0);

                // --- PUSHING ---
                // Only push if we are facing somewhat towards the target or need speed
                float dot = Vector3.Dot(transform.forward, desiredDir);
                if (dot > 0.5f && horizontalVelocity.magnitude < maxSpeed * 0.8f)
                {
                    horizontalVelocity += transform.forward * pushForce * Time.deltaTime * 5f; // Simulating pushes
                }
            }

            // --- GRIP ---
            if (horizontalVelocity.magnitude > 0.1f)
            {
                Vector3 targetVel = transform.forward * horizontalVelocity.magnitude;
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, targetVel, grip * Time.deltaTime);
            }

            // --- FRICTION ---
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, friction * Time.deltaTime);
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        if (horizontalVelocity.magnitude > maxSpeed)
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
    }

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }
    
    public bool IsAtDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }
}
