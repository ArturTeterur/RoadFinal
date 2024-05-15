using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlatformStartingPosition
{
    _zeroDegress,
    _ninetyDegress,
    _oneHungreedAndEightyDegress,
    _twoHundredAndSeventyDegress
}

public class RotationPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] _triggers;
    [SerializeField] private MoveOnPlatformTrigger _platformTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlatformStartingPosition _platformStartingPosition;
    private const string _amimationZeroDegress = "TurnOne";
    private const string _amimationNinetyDegress = "TurnTwo";
    private const string _amimationOneHungreedAndEightyDegress = "TurnThree";
    private const string _animationTwoHundredAndSeventyDegress = "TurnFour";
    private string _currentAnimationTrigger;
    private bool _isRotating = false;

    private void Awake()
    {
        switch(_platformStartingPosition)
        {
            case PlatformStartingPosition._zeroDegress:
                _currentAnimationTrigger = _amimationZeroDegress;
                break;
            case PlatformStartingPosition._ninetyDegress:
                _currentAnimationTrigger = _amimationNinetyDegress;
                break;
            case PlatformStartingPosition._oneHungreedAndEightyDegress:
                _currentAnimationTrigger = _amimationOneHungreedAndEightyDegress;
                break;
            case PlatformStartingPosition._twoHundredAndSeventyDegress:
                _currentAnimationTrigger = _animationTwoHundredAndSeventyDegress;
                break;
        }
    }

    private void RemoveTriggerWhenTurn(bool isRotating)
    {
        for (int i = 0; i < _triggers.Length; i++)
        {
            _triggers[i].SetActive(isRotating);           
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_animator.IsInTransition(0));
        
        _isRotating = false;
    }

    public void Rotate()
    {
        if (!_isRotating)
        {
//RemoveTriggerWhenTurn(false);
            _animator.SetTrigger(_currentAnimationTrigger);
            StartCoroutine("WaitForAnimation");
            _platformTrigger.EnebelingConecters();
        //    RemoveTriggerWhenTurn(true);
        }
    }
}