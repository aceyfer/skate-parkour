using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SkateParkourController : MonoBehaviour
{
    public enum SkateboardStance { Regular, Goofy }

    [Header("Skateboard Physics")]
    public SkateboardStance stance = SkateboardStance.Regular;
    [Tooltip("Speed added per push (W).")]
    [SerializeField] private float pushForce = 8f;
    [Tooltip("Cooldown between pushes.")]
    [SerializeField] private float pushCooldown = 0.5f;
    [Tooltip("Maximum speed achievable by pushing.")]
    [SerializeField] private float maxSpeed = 13f;
[Tooltip("Natural deceleration.")]
    [SerializeField] private float friction = 0.35f;
    [Tooltip("Braking power (S).")]
    [SerializeField] private float brakeForce = 35f;
    [Tooltip("Left/Right steering speed (Degrees per second).")]
    [SerializeField] private float turnSpeed = 250f;
    [Tooltip("How fast the board reaches its target turn speed.")]
    [SerializeField] private float turnTorque = 8f;
    [Tooltip("How much the velocity aligns with the board's forward direction.")]
    [SerializeField] private float grip = 10f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 3.5f;
    [SerializeField] private float gravity = -18f;
    [Tooltip("Control multiplier while in the air.")]
    [SerializeField] private float airControl = 20f;
    [Tooltip("Time to reach full crouch for max jump.")]
    [SerializeField] private float crouchTime = 0.4f;

    [Header("Visuals")]
    [SerializeField] private Transform visualsContainer;
    [SerializeField] private GameObject skateboardMesh;
    private Animator animator;
    private Transform skaterTransform;
    private float impactAmount;
    private bool wasGroundedLastFrame;

    [Header("Off-Board Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float jumpForce = 6f;
    private bool isOnBoard = true;

    [Header("Push Settings")]
    [SerializeField] private float autoPushInterval = 3.0f;
    [SerializeField] private float slowPushThreshold = 2.5f;
    
    private Rigidbody rb;
    private CharacterController controller;
private Transform mainCameraTransform;
    
    private Vector3 horizontalVelocity; 
    private float verticalVelocity;  
    private float lastPushTime;
    private float lastSKeyPressTime;
    private bool sWasPressed;

    private float currentAngularVelocity;
    private float turnAmount; // Smooth -1 to 1 for animation and torque
    
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction sprintAction;

    private bool isCrouching;
    private float crouchAmount; // 0 to 1

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        controller.height = 1.2f;
controller.center = new Vector3(0, 0.6f, 0); 
        
        if (Camera.main != null)
            mainCameraTransform = Camera.main.transform;

        if (visualsContainer == null)
            visualsContainer = transform.Find("Visuals");
        if (visualsContainer == null) visualsContainer = transform.Find("Visuals_Fix");

        if (visualsContainer != null)
        {
            animator = visualsContainer.GetComponentInChildren<Animator>();
            skaterTransform = visualsContainer.Find("Skater_Mesh");
            if (skaterTransform == null)
                skaterTransform = visualsContainer.Find("Skater_Mesh_Detailed");
            
            if (skaterTransform == null)
            {
                var r = visualsContainer.GetComponentInChildren<Renderer>();
                if (r != null) skaterTransform = r.transform;
            }
        }
    }

    public void ResetVelocity()
    {
        horizontalVelocity = Vector3.zero;
        verticalVelocity = 0;
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        UpdateStanceVisuals();
    }

    private void Update()
    {
        if (interactAction != null && interactAction.WasPressedThisFrame())
        {
            ToggleBoard();
        }

        if (isOnBoard)
        {
            HandleSkateMovement();
            HandleJumpAndGravity();
        }
        else
        {
            HandleWalkingMovement();
        }
        
        Vector3 finalMove = horizontalVelocity + Vector3.up * verticalVelocity;
        controller.Move(finalMove * Time.deltaTime);

        // Impact detection
        if (controller.isGrounded && !wasGroundedLastFrame && verticalVelocity < -1f)
        {
            impactAmount = Mathf.Clamp(Mathf.Abs(verticalVelocity) / 15f, 0.3f, 1f);
        }
        wasGroundedLastFrame = controller.isGrounded;
        impactAmount = Mathf.MoveTowards(impactAmount, 0f, Time.deltaTime * 4f);

        ApplyVisuals();
    }

    private void ToggleBoard()
    {
        isOnBoard = !isOnBoard;
        if (skateboardMesh != null) skateboardMesh.SetActive(isOnBoard);
        if (!isOnBoard) horizontalVelocity = Vector3.zero;
        Debug.Log("Board " + (isOnBoard ? "Equipped" : "Unequipped"));
    }

    private void HandleWalkingMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        bool isSprinting = sprintAction != null && sprintAction.IsPressed();
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 camForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(mainCameraTransform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = (camForward * input.y + camRight * input.x).normalized;

        horizontalVelocity = moveDir * currentSpeed;

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 10f * Time.deltaTime);
        }

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0) verticalVelocity = -2f;
            if (jumpAction != null && jumpAction.WasPressedThisFrame())
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void UpdateStanceVisuals()
    {
        ApplyVisuals();
    }

    private void ApplyVisuals()
    {
        if (visualsContainer != null)
        {
            // 1. Skater Lean (The character body tilts into the turn)
            if (skaterTransform != null)
            {
                // Goofy stance is rotated -90, so we apply lean on local X or Z depending on orientation.
                // Based on previous setup, Skater_Mesh is at (-90, -90, 0).
                // Let's use an additive rotation or just set localRotation carefully.
                float bodyLean = turnAmount * -25f;
                skaterTransform.localRotation = Quaternion.Euler(90, 270, bodyLean);
                
                // Landing compression: Shift skater down slightly
                float yOffset = 0.81f - (impactAmount * 0.15f) - (crouchAmount * 0.1f);
                skaterTransform.localPosition = new Vector3(0, yOffset, 0);
            }

            // 2. Board Carve & Pitch
            if (skateboardMesh != null)
            {
                Transform boardT = skateboardMesh.transform;
                float boardRoll = turnAmount * -12f;
                
                // Board pitch (nose up when jumping/falling)
                float targetPitch = 0f;
                if (!controller.isGrounded)
                {
                    targetPitch = Mathf.Clamp(verticalVelocity * 2.5f, -15f, 25f);
                }
                
                // Additive pitch to the base -90 rotation
                boardT.localRotation = Quaternion.Euler(90 + targetPitch, 90, boardRoll);
                
                // Small compression on the board itself
                float boardY = 0.11f - (impactAmount * 0.05f);
                boardT.localPosition = new Vector3(0, boardY, 0);
            }

            // 3. Overall visual compression (Scale)
            float totalCrouch = Mathf.Max(crouchAmount, impactAmount);
            float scaleY = 1.0f - (totalCrouch * 0.15f);
            visualsContainer.localScale = new Vector3(1, scaleY, 1);
        }
    }

    private void HandleSkateMovement()
{
        Vector2 input = moveAction.ReadValue<Vector2>();
        
        // Smooth input for torque and animation
        turnAmount = Mathf.Lerp(turnAmount, input.x, turnTorque * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Velocity", horizontalVelocity.magnitude / maxSpeed);
            animator.SetFloat("TurnAmount", turnAmount);
            animator.SetFloat("CrouchAmount", crouchAmount);
            animator.SetBool("isCrouching", isCrouching);
        }

        if (controller.isGrounded)
{
            // --- STEERING & VELOCITY-LOCKED ROTATION ---
            float targetAngularVelocity = turnAmount * turnSpeed;
            currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetAngularVelocity, turnTorque * Time.deltaTime);
            float turnStep = currentAngularVelocity * Time.deltaTime;

            if (horizontalVelocity.magnitude > 0.1f)
            {
                // In sync steering: Rotate transform and velocity together
                transform.Rotate(0, turnStep, 0);
                horizontalVelocity = Quaternion.Euler(0, turnStep, 0) * horizontalVelocity;

                // Adjust board pitch to ground normal
                Vector3 groundNormal = Vector3.up;
                if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 1.0f))
                {
                    groundNormal = hit.normal;
                }
                
                Vector3 lookDir = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;
                if (lookDir != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(lookDir, groundNormal);
                }
            }
            else
            {
                // Stationary rotation
                transform.Rotate(0, turnStep, 0);
            }

            // Align velocity with forward (Grip)
            if (horizontalVelocity.magnitude > 0.1f)
            {
                Vector3 groundNormal = Vector3.up;
                if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 0.5f))
                {
                    groundNormal = hit.normal;
                }
                
                Vector3 projectedForward = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;
                Vector3 targetVel = projectedForward * horizontalVelocity.magnitude;
                
                // Gravity influence on slopes
                if (groundNormal != Vector3.up)
                {
                    Vector3 slopeGravity = new Vector3(groundNormal.x, 0, groundNormal.z) * gravity * 0.5f;
                    horizontalVelocity += slopeGravity * Time.deltaTime;
                }

                horizontalVelocity = Vector3.Lerp(horizontalVelocity, targetVel, grip * Time.deltaTime);
            }

            // --- PUSHING & AUTO-PUSH ---
            bool isMovingForward = input.y > 0.1f;
            if (isMovingForward && !isCrouching)
            {
                // Contextual slow push animation state
                if (horizontalVelocity.magnitude < slowPushThreshold)
                {
                    if (animator != null) animator.SetBool("isPushing", true);
                }
                else
                {
                    if (animator != null) animator.SetBool("isPushing", false);
                }

                // Automatic push every few seconds
                if (Time.time > lastPushTime + autoPushInterval)
                {
                    horizontalVelocity += transform.forward * pushForce;
                    if (horizontalVelocity.magnitude > maxSpeed)
                        horizontalVelocity = horizontalVelocity.normalized * maxSpeed;

                    if (animator != null) animator.SetTrigger("Push");
                    lastPushTime = Time.time;
                }
            }
            else
            {
                if (animator != null) animator.SetBool("isPushing", false);
            }

            // --- MANUAL PUSH (W) ---
            if (input.y > 0.5f && Time.time > lastPushTime + pushCooldown && !isCrouching)
            {
                horizontalVelocity += transform.forward * pushForce;
                lastPushTime = Time.time;
                if (horizontalVelocity.magnitude > maxSpeed)
                    horizontalVelocity = horizontalVelocity.normalized * maxSpeed;

                if (animator != null) animator.SetTrigger("Push");
            }

            // --- BRAKING (S) ---
            bool sIsPressed = input.y < -0.5f;
            if (sIsPressed)
            {
                horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, brakeForce * Time.deltaTime);
                if (!sWasPressed)
                {
                    if (Time.time - lastSKeyPressTime < 0.3f)
                    {
                        transform.Rotate(0, 180, 0);
                        horizontalVelocity = -horizontalVelocity;
                        lastSKeyPressTime = 0;
                    }
                    else
                    {
                        lastSKeyPressTime = Time.time;
                    }
                }
            }
            sWasPressed = sIsPressed;

            // --- FRICTION ---
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, friction * Time.deltaTime);
        }
        else
        {
            // --- AIR CONTROL ---
            if (Mathf.Abs(input.x) > 0.1f)
                transform.Rotate(0, input.x * turnSpeed * 0.3f * Time.deltaTime, 0);
            
            Vector3 camForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 camRight = Vector3.Scale(mainCameraTransform.right, new Vector3(1, 0, 1)).normalized;
            Vector3 airMoveDir = (camForward * input.y + camRight * input.x).normalized;
            
            horizontalVelocity += airMoveDir * airControl * Time.deltaTime;
            
            if (horizontalVelocity.magnitude > maxSpeed)
                horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
        }
    }

    private void HandleJumpAndGravity()
    {
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0) verticalVelocity = -2f;

            if (jumpAction.IsPressed())
            {
                if (!isCrouching && animator != null) animator.SetBool("isCrouching", true);
                isCrouching = true;
                crouchAmount = Mathf.MoveTowards(crouchAmount, 1f, Time.deltaTime / crouchTime);
            }
            else if (isCrouching)
            {
                // Execute Ollie
                if (animator != null)
                {
                    animator.SetBool("isCrouching", false);
                    animator.SetTrigger("Ollie");
                }
                float jumpPower = 0.5f + (crouchAmount * 0.5f); // Min 50%, Max 100%
                verticalVelocity = Mathf.Sqrt(jumpHeight * jumpPower * -2f * gravity);
                isCrouching = false;
                crouchAmount = 0f;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
            isCrouching = false;
            crouchAmount = 0f;
        }
    }
}






