using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    private const string Coroutine = "MoveTowards";
    [SerializeField] private float _rotationSpeed = 10f;
    private bool _isRotating = false;
    private float _degreeRotation = 90f;
    [SerializeField] private GameObject[] _triggers;
    
    private void RemoveTriggerWhenTurn(bool isRotating)
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].SetActive(isRotating);
        }
    }

    private IEnumerator MoveTowards()
    {
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0, _degreeRotation, 0);
        _isRotating = true;
        RemoveTriggerWhenTurn(false);

        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        _isRotating = false;
        RemoveTriggerWhenTurn(true);

    }
    public void Rotate()
    {
        if (!_isRotating)
        {
            StartCoroutine(Coroutine);
        }
    }
}
