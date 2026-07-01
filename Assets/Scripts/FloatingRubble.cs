using UnityEngine;

public class FloatingRubble : MonoBehaviour
{
    private float bobSpeed;
    private float bobHeight;
    private float rotSpeed;
    private Vector3 startPos;
    private Vector3 rotAxis;

    private void Start()
    {
        startPos = transform.position;
        bobSpeed = Random.Range(0.5f, 1.5f);
        bobHeight = Random.Range(0.1f, 0.3f);
        rotSpeed = Random.Range(5f, 20f);
        rotAxis = Random.onUnitSphere;
    }

    private void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.Rotate(rotAxis, rotSpeed * Time.deltaTime);
    }
}
