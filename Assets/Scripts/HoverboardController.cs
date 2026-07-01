using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class HoverboardController : MonoBehaviour
{
    [Header("Hover Physics")]
    [Tooltip("Target height above the ground.")]
    [SerializeField] private float hoverHeight = 0.6f;
    [Tooltip("Strength of the hover spring.")]
    [SerializeField] private float springStrength = 150f;
    [Tooltip("Damping to prevent oscillation.")]
    [SerializeField] private float damperStrength = 12f;
    [SerializeField] private float gravity = -35f; // Increased gravity for less hangtime

    [Header("Movement")]
    [Tooltip("Continuous acceleration using Psy Energy when holding forward.")]
    [SerializeField] private float acceleration = 20f; // Reduced for Level 1
    [SerializeField] private float maxSpeed = 10f; // Reduced for Level 1
    [SerializeField] private float friction = 0.8f;
    [Tooltip("Turning speed in degrees per second.")]
    [SerializeField] private float turnSpeed = 180f;
    [Tooltip("How strongly the velocity aligns with the forward direction (lower = more drift).")]
    [SerializeField] private float grip = 2.5f;

    [Header("Ollie (Jump)")]
    [Tooltip("Standard height for a consistent tap jump.")]
    [SerializeField] private float tapJumpHeight = 3.5f;
    [Tooltip("Max height achievable with a full Psy-Ollie charge.")]
    [SerializeField] private float maxJumpHeight = 5.5f;
    [SerializeField] private float crouchTime = 0.4f;

    [Header("Off-Board Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float walkJumpForce = 6f;
    [SerializeField] private GameObject hoverboardMesh;
    private bool isOnBoard = true;

    private Animator animator; // Cached animator

    [Header("Audio")]
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.2f;

    private CharacterController controller;
    private Vector3 horizontalVelocity;
    private float verticalVelocity;
    private bool wasGrounded; // To detect landing
    
    private InputAction moveAction;
private InputAction jumpAction;
    private InputAction interactAction;

    private bool isCrouching;
    private float crouchAmount; // 0 to 1
    private bool isHovering;
    private bool canJump; // Buffer for jumping
    private float settleTimer = 0f;
    private const float SettleDuration = 0.5f; // Increased for stability

    public void ResetVelocity()
    {
        horizontalVelocity = Vector3.zero;
        verticalVelocity = 0;
        isCrouching = false;
        crouchAmount = 0;
        settleTimer = SettleDuration;
        
        // Ensure we stop any existing momentum immediately
        if (controller != null)
        {
            controller.Move(Vector3.zero); 
        }
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.height = 1.2f;
        controller.center = new Vector3(0, 0.6f, 0);
        
        // Find animator in children
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");

        ResetVelocity(); // Initialize with settling period
    }

    private void Update()
    {
        if (interactAction != null && interactAction.WasPressedThisFrame())
        {
            ToggleBoard();
        }

        if (isOnBoard)
        {
            HandleHover();
            HandleInput();
            HandleMovement();
            UpdateAnimation();
        }
        else
        {
            HandleWalkingMovement();
        }

        // Final CharacterController move call
        Vector3 finalMove = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(finalMove * Time.deltaTime);

        if (isOnBoard)
        {
            ApplyVisuals();
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null) return;

        float speedPercent = horizontalVelocity.magnitude / maxSpeed;
        animator.SetFloat("Velocity", speedPercent);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetFloat("CrouchAmount", crouchAmount);
        
        // No 'isPushing' or 'Push' trigger as it uses Psy Energy
    }

    [Header("Visuals")]
    [SerializeField] private Transform visualsContainer;
    [SerializeField] private float bobAmplitude = 0.05f;
    [SerializeField] private float bobFrequency = 2f;
    [SerializeField] private float bankAngle = 35f;
    [SerializeField] private float bankSpeed = 5f;

    private float turnAmount; // For smooth banking
    private float currentBank;

    private void LateUpdate()
    {
    }

    private void ApplyVisuals()
    {
        if (visualsContainer == null) return;

        // 1. Hover Bobbing (Procedural sine wave)
        float bob = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        
        // 2. Banking (Roll)
        Vector2 input = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        turnAmount = Mathf.Lerp(turnAmount, input.x, bankSpeed * Time.deltaTime);
        currentBank = Mathf.Lerp(currentBank, turnAmount * -bankAngle, bankSpeed * Time.deltaTime);

        // 3. Jump/Crouch Visuals
        // We let the Animator handle the crouch visuals via parameters.
        // The container no longer squashes to avoid 'retarded' board visuals.
        
        // Apply all to visualsContainer
        visualsContainer.localPosition = new Vector3(0, bob, 0);
        visualsContainer.localRotation = Quaternion.Euler(0, 0, currentBank);
        visualsContainer.localScale = Vector3.one;
    }

    private void ToggleBoard()
    {
        isOnBoard = !isOnBoard;
        if (hoverboardMesh != null) hoverboardMesh.SetActive(isOnBoard);
        if (!isOnBoard) 
        {
            horizontalVelocity = Vector3.zero;
            isCrouching = false;
            crouchAmount = 0;
        }
        Debug.Log("Hoverboard " + (isOnBoard ? "Equipped" : "Unequipped"));
    }

    private void HandleWalkingMovement()
    {
        Vector2 input = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
        
        Vector3 camForward = Vector3.forward;
        Vector3 camRight = Vector3.right;
        if (Camera.main != null)
        {
            camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            camRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        }

        Vector3 moveDir = (camForward * input.y + camRight * input.x).normalized;
        horizontalVelocity = moveDir * walkSpeed;

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 12f * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0) verticalVelocity = -2f;
            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
                verticalVelocity = walkJumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void HandleHover()
    {
        if (settleTimer > 0) settleTimer -= Time.deltaTime;

        bool isCurrentlyHovering = false;
        bool isCloseToGround = false;

        // Raycast from slightly above the board's base center
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, hoverHeight + 2.0f))
        {
            float distance = hit.distance - 0.1f; 
            float error = hoverHeight - distance;
            
            // canJump has a larger tolerance than isHovering to feel more responsive
            isCloseToGround = distance < hoverHeight + 0.5f;

            if (error > 0)
            {
                isCurrentlyHovering = true;
                // Spring-damper force calculation: F = k*x - c*v
                float springForce = error * springStrength;
                float dampingForce = verticalVelocity * damperStrength;
                float hoverForce = springForce - dampingForce;
                
                // Extra damping during settling period
                if (settleTimer > 0)
                {
                    hoverForce = Mathf.Min(hoverForce, 10f); // Very weak force during settling
                }
                else
                {
                    // Clamp the upward force to prevent "super bounces"
                    hoverForce = Mathf.Min(hoverForce, 50f);
                }
                
                verticalVelocity += hoverForce * Time.deltaTime;
            }
            else
            {
                // Above hover height: apply gravity
                // We consider it 'hovering' if close enough to the ground to steer
                isCurrentlyHovering = distance < hoverHeight + 0.15f;
                verticalVelocity += gravity * Time.deltaTime;
            }
        }
        else
        {
            isCurrentlyHovering = false;
            isCloseToGround = false;
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Landing Sound
        if (isCurrentlyHovering && !wasGrounded && settleTimer <= 0)
        {
            if (sfxSource != null && landSound != null) sfxSource.PlayOneShot(landSound);
        }
        wasGrounded = isCurrentlyHovering;
        isHovering = isCurrentlyHovering;
        canJump = isCloseToGround;

        // Hover Hum
        if (loopSource != null)
        {
            float speedPercent = horizontalVelocity.magnitude / maxSpeed;
            loopSource.pitch = Mathf.Lerp(minPitch, maxPitch, speedPercent);
            if (isCurrentlyHovering && !loopSource.isPlaying) loopSource.Play();
            else if (!isCurrentlyHovering && loopSource.isPlaying) loopSource.Stop();
        }

        // Safety clamp for vertical velocity
        verticalVelocity = Mathf.Clamp(verticalVelocity, gravity * 2f, 50f);
    }

    private void HandleInput()
    {
        if (jumpAction != null)
        {
            // Only allow starting a crouch if we are close to ground
            if (jumpAction.WasPressedThisFrame() && canJump)
            {
                isCrouching = true;
                crouchAmount = 0f;
            }

            if (isCrouching)
            {
                if (jumpAction.IsPressed())
                {
                    // Charging
                    crouchAmount = Mathf.MoveTowards(crouchAmount, 1f, Time.deltaTime / crouchTime);
                }
                else
                {
                    // Executing jump on release
                    // Only jump if we are still relatively close to ground
                    if (canJump)
                    {
                        float jumpPower = Mathf.Lerp(0f, 1f, crouchAmount);
                        float finalJumpHeight = Mathf.Max(tapJumpHeight, Mathf.Lerp(tapJumpHeight, maxJumpHeight, jumpPower));
                        
                        verticalVelocity = Mathf.Sqrt(finalJumpHeight * -2f * gravity);
                        
                        if (animator != null) animator.SetTrigger("Ollie");
                        if (sfxSource != null && jumpSound != null) sfxSource.PlayOneShot(jumpSound);
                    }
                    
                    isCrouching = false;
                    crouchAmount = 0f;
                }
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;

        // 1. Steering: Rotate the board and its existing horizontal velocity
        float turnStep = input.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnStep, 0);
        horizontalVelocity = Quaternion.Euler(0, turnStep, 0) * horizontalVelocity;

        if (isHovering)
        {
            // 2. Continuous Acceleration (W/S)
            if (Mathf.Abs(input.y) > 0.1f)
            {
                float accelMult = input.y > 0 ? 1f : 0.6f; // Slower in reverse
                horizontalVelocity += transform.forward * acceleration * accelMult * input.y * Time.deltaTime;
            }

            // 3. Friction
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, friction * Time.deltaTime);

            // 4. Low Grip (Drifting)
            // Gradually align the velocity vector with the board's forward direction
            if (horizontalVelocity.magnitude > 0.1f)
            {
                Vector3 targetVel = transform.forward * horizontalVelocity.magnitude;
                horizontalVelocity = Vector3.Lerp(horizontalVelocity, targetVel, grip * Time.deltaTime);
            }
        }
        else
        {
            // 5. Air Control (Minimal nudge while in the air)
            if (Mathf.Abs(input.y) > 0.1f || Mathf.Abs(input.x) > 0.1f)
            {
                Vector3 airMoveDir = (transform.forward * input.y + transform.right * input.x).normalized;
                horizontalVelocity += airMoveDir * (acceleration * 0.15f) * Time.deltaTime;
            }
        }

        // 6. Max Speed Limit
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
        }
    }
}
