using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    Rigidbody rb;
    float initialElevation;
    public float Elevation { private get; set; }


    private void Start()
    {
        initialElevation = this.transform.position.y;
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 temp = new Vector3(this.transform.position.x, initialElevation + Elevation, this.transform.position.z);
        rb.MovePosition(temp +
            (Vector3.forward * Time.deltaTime * 30f));
        
    }
}
