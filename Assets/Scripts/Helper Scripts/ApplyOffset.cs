using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyOffset : MonoBehaviour
{
    [SerializeField] private Transform offsetFrom;
    private Vector3 offset;

    private void Start()
    {
        offset = this.transform.position - offsetFrom.position;
    }


    private void Update()
    {
        this.transform.position = offsetFrom.position + offset;    
    }
}
