using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTargetting : MonoBehaviour
{

    // The Target for the limb
    [SerializeField] private Transform limbTarget;
    [SerializeField] private int threshold = 40;

    private Vector3 moveTowards;
    Vector3 vel = Vector3.zero;

    void Update()
    {
        var distance = (this.transform.position - limbTarget.transform.position).sqrMagnitude;

        if (distance > threshold * threshold)
        {
            StopAllCoroutines();
            StartCoroutine("MoveTowardsTarget", limbTarget.transform.position);
        }
    }

    IEnumerator MoveTowardsTarget(Vector3 target)
    {
        while (true)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref vel, .1f);

            if ((this.transform.position - target).sqrMagnitude <= .01f)
                break;
            
            yield return null;
        }

        yield return null ;
    }


}
