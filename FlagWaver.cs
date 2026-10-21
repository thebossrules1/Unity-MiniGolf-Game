using UnityEngine;

public class FlagWaver : MonoBehaviour
{
    public float waveSpeed = 5f;
    public float waveAmount = 10f;

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
    }

    void Update()
    {
        // Gently rotates the flag back and forth on the Y axis to simulate wind
        float yaw = Mathf.Sin(Time.time * waveSpeed) * waveAmount;
        transform.localEulerAngles = initialRotation + new Vector3(0, yaw, 0);
    }
}