using UnityEngine;

public class Cable : MonoBehaviour
{
    public Transform startTransform; // The starting point of the cable
    public Transform endTransform; // The ending point of the cable

    public int segmentCount = 10; // Number of segments in the cable
    public float segmentLength = 0.5f; // Length of each segment
    public float cableWidth = 0.1f; // Width of the cable

    public float curvature = 0.5f; // Curvature factor

    private LineRenderer lineRenderer; // Reference to the LineRenderer component

    private void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        lineRenderer.startWidth = cableWidth;
        lineRenderer.endWidth = cableWidth;

        // Generate initial cable shape
        UpdateCable();
    }

    private void Update()
    {
        // Update the cable shape if the startTransform has moved
        UpdateCable();
    }

    private void UpdateCable()
    {
        // Calculate the direction and length of the cable
        Vector3 direction = (endTransform.position - startTransform.position).normalized;
        float cableLength = Vector3.Distance(startTransform.position, endTransform.position);

        // Calculate the position of each cable segment
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount; // Interpolation parameter

            // Calculate the position of the current segment
            Vector3 segmentPosition = startTransform.position + direction * (t * cableLength);

            // Apply physics to the segment to simulate cable behavior
            RaycastHit hit;
            if (Physics.Raycast(segmentPosition, -direction, out hit, segmentLength))
            {
                // Adjust the position of the segment based on the collision point
                segmentPosition = hit.point + direction * cableWidth;
            }

            // Apply curvature to the cable segment
            float curvatureOffset = Mathf.Sin(t * Mathf.PI) * curvature;
            Vector3 curvatureOffsetVector = Vector3.Cross(direction, Vector3.up) * curvatureOffset;
            segmentPosition += curvatureOffsetVector;

            // Set the position of the LineRenderer point
            lineRenderer.SetPosition(i, segmentPosition);
        }
    }
}