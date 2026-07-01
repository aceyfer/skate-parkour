using UnityEngine;

public class PsyHoverVFX : MonoBehaviour
{
    [Header("Trail Settings")]
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private float minWidth = 0.1f;
    [SerializeField] private float maxWidth = 0.3f;

    [Header("Pulse Settings")]
    [SerializeField] private Light pulseLight;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 2.0f;
    [SerializeField] private float pulseSpeed = 4f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime * pulseSpeed;
        float pulse = (Mathf.Sin(timer) + 1f) / 2f; // 0 to 1

        if (pulseLight != null)
        {
            pulseLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);
        }

        if (trail != null)
        {
            trail.widthMultiplier = Mathf.Lerp(minWidth, maxWidth, pulse);
        }
    }
}
