using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnglePlatform
{
    PositionUpLeft,
    PositionUpRight,
    PositionDownRight,
    PositionDownLeft
}

public class RotationPlatform : MonoBehaviour
{
    private const string Coroutine = "MoveTowards";
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private GameObject[] _triggers;
    [SerializeField] private int _currentCountTurn = 0;
    [SerializeField] private bool _correctPositionPlatform = false;
    [SerializeField] private MoveOnPlatformTrigger _platformTrigger;
    [SerializeField] private bool _thisAnglePlatform;
    [SerializeField] private AnglePlatform _correctPlatform;
    [SerializeField] private AnglePlatform _currentPlatform;
   
    private float _degreeRotation = 90f;
    private bool _isRotating = false;
    public bool CorrectPositionPlatform => _correctPositionPlatform;

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
        _platformTrigger.DestroyObjectsOnPlatform();

        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }
        _isRotating = false;
        CheckCurrentPlatform();
        RemoveTriggerWhenTurn(true);
    }

    public void Rotate()
    {
        if (!_isRotating)
        {
            StartCoroutine(Coroutine);
        }
    }

    private void CheckCurrentPlatform()
    {
        if (_thisAnglePlatform)
        {
            CheckRotationAnglePlatform();
        }
        else
        {
            CheckRotationStraightPlatform();
        }
    }

    private void CheckRotationStraightPlatform()
    {
        if (_correctPositionPlatform)
        {
            _correctPositionPlatform = false;
        }
        else
        {
            _correctPositionPlatform = true;
        }
    }

    private void CheckRotationAnglePlatform()
    {
        _currentPlatform = (AnglePlatform)(((int)_currentPlatform + 1) % 4);

        if (_correctPlatform == _currentPlatform)
        {
            _correctPositionPlatform = true;
        }
        else
        {
            _correctPositionPlatform = false;
        }
    }
}
