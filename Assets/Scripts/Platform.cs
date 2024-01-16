using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private float platformRotationDegree = 90f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private void Start()
    {
        initialRotation = transform.rotation;
    }
    public void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0f, platformRotationDegree, 0f);         
    }
}
