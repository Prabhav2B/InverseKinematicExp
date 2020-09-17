using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class HandleTargetting : MonoBehaviour
{

    public bool isMoving = false;

    // The Target for the limb
    [SerializeField] private Transform limbTarget;
    [SerializeField] private GameObject oppositeTarget;
    [SerializeField] private int threshold = 40;

    public GlobalScript legMovement;

    private Vector3 moveTowards;
    Vector3 vel = Vector3.zero;
    
    private float timeToReach = 0.175f;
    private float legHeightChange = 3;


    private float timeElapsed = 0f;
    private Vector3 originalPosition;

    void Update()
    {
        var distance = (this.transform.position - limbTarget.transform.position).sqrMagnitude;

        HandleTargetting otherScript = oppositeTarget.GetComponentInChildren<HandleTargetting>();

        if (distance > threshold * threshold && !otherScript.isMoving && !isMoving)
        {
            timeElapsed = 0f;
            originalPosition = this.transform.position;
            isMoving = true;
            moveTowards = limbTarget.position;
        } else if (isMoving)
        {
            timeElapsed += Time.deltaTime;

            float percentCompleted = timeElapsed / timeToReach;
            float curveValue = legMovement.curve.Evaluate(percentCompleted);

            this.transform.position = new Vector3(
                originalPosition.x * (1 - percentCompleted) + moveTowards.x * percentCompleted,
                originalPosition.y * (1 - percentCompleted) + moveTowards.y * percentCompleted + (curveValue * legHeightChange),
                originalPosition.z * (1 - percentCompleted) + moveTowards.z * percentCompleted
            );

            if (timeElapsed >= timeToReach)
            {
                isMoving = false;
            }
        } 
    }

}
