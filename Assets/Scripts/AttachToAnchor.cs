using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToAnchor : MonoBehaviour
{
    [SerializeField] private Transform limbAnchor;
    private void Update()
    {
        this.transform.position = limbAnchor.position;
    }
}
