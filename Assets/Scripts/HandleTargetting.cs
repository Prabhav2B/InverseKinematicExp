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
    
    private float speed = 0.1f;

    void Update()
    {
        var distance = (this.transform.position - limbTarget.transform.position).sqrMagnitude;

        HandleTargetting otherScript = oppositeTarget.GetComponent<HandleTargetting>();

        if (distance > threshold * threshold && !otherScript.isMoving)
        {
            speed = 10 / distance;
            Debug.Log(distance);
            StopAllCoroutines();
            StartCoroutine("MoveTowardsTarget", limbTarget.transform.position);
        }
    }

    IEnumerator MoveTowardsTarget(Vector3 target)
    {
        isMoving = true;
        while (true)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref vel, speed);

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
