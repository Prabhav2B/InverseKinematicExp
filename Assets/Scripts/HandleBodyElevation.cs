using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBodyElevation : MonoBehaviour
{
    [SerializeField]private Transform targetParent;
    private HandleTargetting[] targets;
    float initialElevation;
    float meanTargetInitalElevation;
    Rigidbody rb;

    private void Start()
    {
        initialElevation = this.transform.position.y;
        targets = targetParent.GetComponentsInChildren<HandleTargetting>();

        rb = GetComponent<Rigidbody>();

        float sum = 0;
        foreach (var target in targets)
        {
            sum += target.transform.position.y;
        }

        meanTargetInitalElevation = sum / targets.Length;
    }

    private void FixedUpdate()
    {
        float elevation = CalculateAverageElevation();
        this.transform.position = new Vector3(this.transform.position.x, initialElevation + elevation, this.transform.position.z);
        //rb.MovePosition(new Vector3(this.transform.position.x, initialElevation + elevation, this.transform.position.z));
    }

    private float CalculateAverageElevation()
    {
        float sum = 0;
        foreach (var target in targets)
        {
            sum += (target.transform.position.y - meanTargetInitalElevation);
        }

       return(sum / targets.Length);
    }
}
