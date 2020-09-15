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

    private Vector3 moveTowards;
    Vector3 vel = Vector3.zero;
    
    private float timeToReach = 0.1f;

    void Update()
    {
        var distance = (this.transform.position - limbTarget.transform.position).sqrMagnitude;

        HandleTargetting otherScript = oppositeTarget.GetComponentInChildren<HandleTargetting>();

        if (distance > threshold * threshold && !otherScript.isMoving)
        {
            //timeToReach =  distance/1000f; Needs to be fixed/rethought
            //Debug.Log(distance);
            StopAllCoroutines();
            StartCoroutine("MoveTowardsTarget", limbTarget.transform.position);
        }
    }

    IEnumerator MoveTowardsTarget(Vector3 target)
    {
        isMoving = true;
        while (true)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref vel, timeToReach);

            if ((this.transform.position - target).sqrMagnitude <= .01f)
            {
                isMoving = false;
                break;
            }
            
            yield return null;
        }

        yield return null ;
    }


}
