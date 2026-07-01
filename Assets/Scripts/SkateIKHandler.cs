using UnityEngine;

public class SkateIKHandler : MonoBehaviour
{
    private Animator animator;
    
    [Header("IK Targets")]
    public Transform leftFootTarget;
    public Transform rightFootTarget;
    
    [Header("IK Settings")]
    [Range(0, 1)] public float ikWeight = 1.0f;
    public float footHeightOffset = 0.05f;
    
    [Header("Spring Settings")]
    public float springFrequency = 15f;
    public float springDamping = 0.5f;
    
    private Vector3 leftFootPos;
    private Vector3 rightFootPos;
    private Vector3 leftFootVel;
    private Vector3 rightFootVel;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (leftFootTarget != null) leftFootPos = leftFootTarget.position;
        if (rightFootTarget != null) rightFootPos = rightFootTarget.position;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null || ikWeight <= 0) return;

        ApplyIK(AvatarIKGoal.LeftFoot, leftFootTarget, ref leftFootPos, ref leftFootVel);
        ApplyIK(AvatarIKGoal.RightFoot, rightFootTarget, ref rightFootPos, ref rightFootVel);
    }

    private void ApplyIK(AvatarIKGoal goal, Transform target, ref Vector3 currentPos, ref Vector3 velocity)
    {
        if (target == null) return;

        animator.SetIKPositionWeight(goal, ikWeight);
        animator.SetIKRotationWeight(goal, ikWeight);

        // Spring target position
        Vector3 targetPos = target.position + target.up * footHeightOffset;
        
        // Simple spring math (using SmoothDamp as a proxy for spring behavior)
        float smoothTime = 1f / springFrequency;
        currentPos = Vector3.SmoothDamp(currentPos, targetPos, ref velocity, smoothTime, 100f, Time.deltaTime);

        animator.SetIKPosition(goal, currentPos);
        animator.SetIKRotation(goal, target.rotation);
    }
}
