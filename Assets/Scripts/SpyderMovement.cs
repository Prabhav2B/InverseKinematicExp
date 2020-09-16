using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.MovePosition(this.transform.position + (Vector3.forward * Time.deltaTime * 30f));
    }
}
