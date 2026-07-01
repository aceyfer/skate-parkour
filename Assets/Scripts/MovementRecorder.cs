using UnityEngine;
using System.Collections.Generic;

public class MovementRecorder : MonoBehaviour
{
    [System.Serializable]
    public struct MovementFrame
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;
    }

    [Header("Settings")]
    public Transform targetToRecord;
    public float recordInterval = 0.1f;
    public int maxFrames = 1000;

    private List<MovementFrame> recordedFrames = new List<MovementFrame>();
    private bool isRecording = false;
    private float lastRecordTime = 0f;
    private float recordingStartTime = 0f;

    public List<MovementFrame> RecordedFrames => recordedFrames;
    public bool IsRecording => isRecording;

    private void Update()
    {
        if (isRecording)
        {
            if (Time.time >= lastRecordTime + recordInterval)
            {
                RecordFrame();
            }
        }
    }

    public void StartRecording()
    {
        if (targetToRecord == null)
        {
            Debug.LogWarning("MovementRecorder: No target to record!");
            return;
        }

        recordedFrames.Clear();
        isRecording = true;
        recordingStartTime = Time.time;
        RecordFrame();
        Debug.Log("MovementRecorder: Recording started.");
    }

    public void StopRecording()
    {
        isRecording = false;
        Debug.Log("MovementRecorder: Recording stopped. Captured " + recordedFrames.Count + " frames.");
    }

    private void RecordFrame()
    {
        if (recordedFrames.Count >= maxFrames)
        {
            StopRecording();
            return;
        }

        MovementFrame frame = new MovementFrame
        {
            position = targetToRecord.position,
            rotation = targetToRecord.rotation,
            time = Time.time - recordingStartTime
        };

        recordedFrames.Add(frame);
        lastRecordTime = Time.time;
    }
}
