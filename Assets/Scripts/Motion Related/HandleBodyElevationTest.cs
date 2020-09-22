
using UnityEngine;

public class HandleBodyElevationTest : MonoBehaviour
{
    [SerializeField] private float targetElevation = 12f;
    [SerializeField]private LayerMask groundlayer = 1 >> 8;

    private Vector3 offset;

    SpyderMovement spyderMovement;
    float testDistance = 0f;
    float hitDistance;

    private void Start()
    {
        offset = Vector3.up * 25;
        spyderMovement = GetComponent<SpyderMovement>();
    }

    private void FixedUpdate()
    {
        spyderMovement.Elevation = CalculateAverageElevation();
    }

    private float CalculateAverageElevation()
    {
        Ray ray = new Ray(this.transform.position + offset, -this.transform.up);
        RaycastHit hit;

        float elevation;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundlayer))
        {

            float distanceToGround = hit.distance - offset.y;
            hitDistance = distanceToGround;
            elevation = targetElevation - distanceToGround;
        }
        else
        {
            Debug.Log("Piece of Shit");
            elevation = 0f;
        }

        return elevation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, -transform.up * hitDistance);
    }

}
