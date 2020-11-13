using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyderMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private bool playerControlled = true;


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
            input = this.transform.rotation * new Vector3(InputX, 0, InputZ);
            Vector3 target = new Vector3(this.transform.position.x, initialElevation + Elevation, this.transform.position.z);

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
            //non-player controlled movement here
        }
        
    }
}
