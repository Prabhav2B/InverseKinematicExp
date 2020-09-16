using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    Rigidbody rb;
    private float elevation;

    [Space(15)]
    public AnimationCurve curveMovement = new AnimationCurve();

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.MovePosition(this.transform.position + (Vector3.forward * Time.deltaTime * 30f));
    }
}
