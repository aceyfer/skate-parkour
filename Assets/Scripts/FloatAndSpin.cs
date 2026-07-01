using UnityEngine;

public class FloatAndSpin : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.2f;
    public Vector3 spinAxis = Vector3.up;
    public float spinSpeed = 10f;
    public bool useWorldSpace = false;

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        // Floating
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPos + Vector3.up * newY;

        // Spinning
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime, useWorldSpace ? Space.World : Space.Self);
    }
}
