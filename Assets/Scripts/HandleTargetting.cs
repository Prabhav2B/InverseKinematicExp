using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTargetting : MonoBehaviour
{

    // The Target for the limb
    public Transform ActualTarget;
    public int Threshold = 10;

    private Vector3 MoveTowards;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distance = (this.transform.position - ActualTarget.transform.position).sqrMagnitude;

        if (distance * distance > Threshold * Threshold) {
            MoveTowards = this.transform.position;
            ActualTarget.transform.position = Vector3.MoveTowards(ActualTarget.transform.position, MoveTowards, 0.5f);
        }
    }
}
