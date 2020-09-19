using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private bool playerControlled = false;


    Rigidbody rb;
    float initialElevation;
    public float Elevation { private get; set; }

    public float InputX { get; private set; }
    public float InputZ { get; private set; }



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
            Vector3 localChangeInElevation = new Vector3(0f, Elevation, 0f);
            Vector3 target = this.transform.TransformPoint(localChangeInElevation);

            Vector3 elevation = target - this.transform.position ;
            Vector3 direction = (input + elevation).normalized;


            if (InputX != 0 || InputZ != 0)
            {
                rb.MovePosition(this.transform.position + 
                    (direction * Time.fixedDeltaTime * movementSpeed));
            }
        }
        else
        {
            input = Vector3.forward;
        }
        
    }
}
