using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleElevation : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    [SerializeField] private LayerMask layerMask = 1>>1;
    [SerializeField] private float elevationFromGround = 0.5f;

    private void FixedUpdate()
    {
        ray = new Ray(this.transform.position + (Vector3.up * 0.5f), Vector3.down);


        if (Physics.Raycast(ray, out hit, layerMask))
        {
            this.transform.position = new Vector3(this.transform.position.x, hit.point.y, this.transform.position.z );
        }
        else
        {
            Debug.Log("oof");
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.DrawRay(ray);
#endif
    }

}
