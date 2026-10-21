using UnityEngine;

public class WindmillSpinner : MonoBehaviour
{
    [Tooltip("How fast the windmill spins")]
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate around the Z-axis (adjust if your model needs X or Y instead)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}