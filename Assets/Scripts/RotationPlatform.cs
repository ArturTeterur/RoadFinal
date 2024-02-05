using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    private bool isRotating = false;
    private float _degreeRotation = 90f;

    public void Rotate()
    {
        if (!isRotating)
        {
            StartCoroutine("MoveTowards");
        }
    }

    private IEnumerator MoveTowards()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0, _degreeRotation, 0);
        isRotating = true;

        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
        isRotating = false;
    }
}
