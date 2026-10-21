using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class GolfBallController : MonoBehaviour
{
    [Header("Golf Physics Settings")]
    [SerializeField] private float maxPower = 15f;
    [SerializeField] private float powerMultiplier = 3f;
    [SerializeField] private float minVelocityToStop = 0.1f;

    private Rigidbody rb;
    private LineRenderer lineRenderer;
    private Camera mainCamera;

    private Vector3 dragStartPosition;
    private bool isDragging = false;
    private bool isBallMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        // Check if ball has completely stopped moving before allowing next shot
        if (isBallMoving)
        {
            if (rb.linearVelocity.magnitude < minVelocityToStop && rb.angularVelocity.magnitude < minVelocityToStop)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                isBallMoving = false;
            }
            return; // Can't aim while moving
        }

        HandleInput();
    }

    private void HandleInput()
    {
        // Start dragging on click
        if (Input.GetMouseButtonDown(0))
        {
            Vector3? mouseWorldPos = GetMouseWorldPosition();
            if (mouseWorldPos.HasValue)
            {
                isDragging = true;
                dragStartPosition = mouseWorldPos.Value;
                lineRenderer.enabled = true;
            }
        }

        // Draw trajectory line while dragging
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3? currentMousePos = GetMouseWorldPosition();
            if (currentMousePos.HasValue)
            {
                // Aim line points opposite of drag direction
                Vector3 dragDirection = dragStartPosition - currentMousePos.Value;
                dragDirection.y = 0; // Lock to horizontal plane

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + dragDirection);
            }
        }

        // Release to shoot
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Vector3? releaseMousePos = GetMouseWorldPosition();
            if (releaseMousePos.HasValue)
            {
                Vector3 shotVector = dragStartPosition - releaseMousePos.Value;
                shotVector.y = 0; // Prevent accidental upward force

                Vector3 force = Vector3.ClampMagnitude(shotVector * powerMultiplier, maxPower);
                rb.AddForce(force, ForceMode.Impulse);

                // Increment stroke count on every successful shot release
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddStroke();
                }

                isBallMoving = true;
            }

            isDragging = false;
            lineRenderer.enabled = false;
        }
    }

    private Vector3? GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return null;
    }
}