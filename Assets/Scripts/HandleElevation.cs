using System;
using UnityEngine;

public class HandleElevation : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    [SerializeField] private LayerMask layerMask;

    private float groundSlopeAngle = 0f;            // Angle of the slope in degrees
    private float groundSlopeElevation = 0f;            // Elevation of the slope in global coordinates
    private Vector3 groundSlopeDir = Vector3.zero;  // The calculated slope as a vector

    [Header("Settings")]
    public bool showDebug = false;                  // Show debug gizmos and lines
    
    [SerializeField] private float startDistanceFromBottom = 0.5f;   // Should probably be higher than skin width
    [SerializeField] private float sphereCastRadius = 0.25f;
    [SerializeField] private float sphereCastDistance = 0.75f;       // How far spherecast moves down from origin point

    [SerializeField] private float raycastLength = 0.75f;
    [SerializeField] private Vector3 rayOriginOffset1 = new Vector3(-0.2f, 0f, 0.16f);
    [SerializeField] private Vector3 rayOriginOffset2 = new Vector3(0.2f, 0f, -0.16f);

    void FixedUpdate()
    {
        Vector3 origin = new Vector3(transform.position.x,
            transform.position.y + startDistanceFromBottom,
            transform.position.z);

        if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, layerMask))
        {
            float height = CheckGround(new Vector3(transform.position.x,
            transform.position.y + startDistanceFromBottom,
            transform.position.z));

            {
                this.transform.position = new Vector3(transform.position.x,
            groundSlopeElevation,
            transform.position.z);
            }
        }
    }

    /// <summary>
    /// Checks for ground underneath, to determine some info about it, including the slope angle.
    /// </summary>
    /// <param name="origin">Point to start checking downwards from</param>
    public float CheckGround(Vector3 origin)
    {
        // Out hit point from our cast(s)
        RaycastHit hit;

        Vector3 hitPoint1 = Vector3.zero;
        Vector3 hitPoint2 = Vector3.zero;
        Vector3 hitPoint3 = Vector3.zero;

        // SPHERECAST
        // "Casts a sphere along a ray and returns detailed information on what was hit."
        if (Physics.SphereCast(origin, sphereCastRadius, Vector3.down, out hit, sphereCastDistance, layerMask))
        {
            // Angle of our slope (between these two vectors). 
            // A hit normal is at a 90 degree angle from the surface that is collided with (at the point of collision).
            // e.g. On a flat surface, both vectors are facing straight up, so the angle is 0.
            groundSlopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            // Find the vector that represents our slope as well. 
            //  temp: basically, finds vector moving across hit surface 
            Vector3 temp = Vector3.Cross(hit.normal, Vector3.down);
            //  Now use this vector and the hit normal, to find the other vector moving up and down the hit surface
            groundSlopeDir = Vector3.Cross(temp, hit.normal);

            hitPoint1 = hit.point;
        }

        // Now that's all fine and dandy, but on edges, corners, etc, we get angle values that we don't want.
        // To correct for this, let's do some raycasts. You could do more raycasts, and check for more
        // edge cases here. There are lots of situations that could pop up, so test and see what gives you trouble.
        RaycastHit slopeHit1;
        RaycastHit slopeHit2;

        // FIRST RAYCAST
        if (Physics.Raycast(origin + rayOriginOffset1, Vector3.down, out slopeHit1, raycastLength))
        {
            // Debug line to first hit point
            if (showDebug) { Debug.DrawLine(origin + rayOriginOffset1, slopeHit1.point, Color.red); }
            // Get angle of slope on hit normal
            float angleOne = Vector3.Angle(slopeHit1.normal, Vector3.up);

            hitPoint2 = slopeHit1.point;

            // 2ND RAYCAST
            if (Physics.Raycast(origin + rayOriginOffset2, Vector3.down, out slopeHit2, raycastLength))
            {
                // Debug line to second hit point
                if (showDebug) { Debug.DrawLine(origin + rayOriginOffset2, slopeHit2.point, Color.red); }
                // Get angle of slope of these two hit points.
                float angleTwo = Vector3.Angle(slopeHit2.normal, Vector3.up);

                hitPoint3 = slopeHit2.point;

                // 3 collision points: Take the MEDIAN by sorting array and grabbing middle.
                float[] tempArray = new float[] { hitPoint1.y, hitPoint2.y, hitPoint3.y };
                Array.Sort(tempArray);
                groundSlopeElevation = tempArray[1];
            }
            else
            {
                // 2 collision points (sphere and first raycast): AVERAGE the two
                float average = (hitPoint1.y + hitPoint2.y) / 2;
                groundSlopeElevation = average;
            }
        }

        return groundSlopeElevation;

    }

    void OnDrawGizmosSelected()
    {
        if (showDebug)
        {
            // Visualize SphereCast with two spheres and a line
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + startDistanceFromBottom, transform.position.z);
            Vector3 endPoint = new Vector3(transform.position.x, transform.position.y + startDistanceFromBottom - sphereCastDistance, transform.position.z);

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(startPoint, sphereCastRadius);

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(endPoint, sphereCastRadius);

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}
