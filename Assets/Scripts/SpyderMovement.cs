using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    Rigidbody rb;
    float initialElevation;
    public float Elevation { private get; set; }

    public float InputX { get; private set; }
    public float InputZ { get; private set; }

    [SerializeField] private bool playerControlled = false;


    private void Start()
    {
        initialElevation = this.transform.position.y;
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 input;

        if (playerControlled)
        {
            input = new Vector3(InputX, 0, InputZ);
        }
        else
        {
            input = Vector3.forward;
        }

        Vector3 temp = new Vector3(this.transform.position.x, initialElevation + Elevation, this.transform.position.z);
        rb.MovePosition(temp +
            (input * Time.deltaTime * 10f));
        
    }
}
