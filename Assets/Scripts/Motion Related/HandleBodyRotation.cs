using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBodyRotation : MonoBehaviour
{

    // Order is BackLeft [0], BackRight [1], FrontLeft [2], FrontRight [3]
    // Front is + on the Z axis, right is + on the X axis
    [SerializeField] private Transform[] targets;
    [Range(0f, 1f)]
    [SerializeField] private float rotationDamp = 0.8f;
    // Start is called before the first frame update
    [SerializeField]private Transform rotationReference;

    // Update is called once per frame
    void FixedUpdate()
    {
        // The average y coordinate difference between back and front
        float backFrontDifference = (((targets[2].position.y + targets[3].position.y) / 2) -
            ((targets[0].position.y + targets[1].position.y) / 2));

        // Average z distance from backlegs to frontlegs
        float averageZDistance = (((targets[2].position.z + targets[3].position.z) / 2) -
            ((targets[0].position.z + targets[1].position.z) / 2));

        float leftRightDifference = (((targets[0].position.y + targets[2].position.y) / 2) -
            ((targets[1].position.y + targets[3].position.y) / 2));

        float averageXDistance = (((targets[0].position.x + targets[2].position.x) / 2) -
            ((targets[1].position.x + targets[3].position.x) / 2));



        this.GetComponent<Rigidbody>().MoveRotation(
              Quaternion.Euler(
                -1 * Mathf.Atan(backFrontDifference / averageZDistance * (1 - rotationDamp)) * 180/Mathf.PI, 
                rotationReference.transform.rotation.y * 180/Mathf.PI,
                Mathf.Atan(leftRightDifference / averageXDistance * (1 - rotationDamp)) * 180 / Mathf.PI
            )
        );
    }
}
