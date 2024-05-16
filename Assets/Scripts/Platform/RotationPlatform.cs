using System.Collections;
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
    private const string WaitForAnimationName = "WaitForAnimation";
    private const string _amimationZeroDegress = "TurnZeroDegress";
    private const string _amimationNinetyDegress = "TurnNinetyDegress";
    private const string _amimationOneHungreedAndEightyDegress = "TurnOneHungreedAndEightyDegress";
    private const string _animationTwoHundredAndSeventyDegress = "TurnTwoHundredAndSeventyDegress";
    [SerializeField] private GameObject[] _triggers;
    [SerializeField] private CheckingMovementBalls _platformTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlatformStartingPosition _platformStartingPosition;
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

    public void Rotate()
    {
        if (!_isRotating)
        {
            _animator.SetTrigger(_currentAnimationTrigger);
            StartCoroutine(WaitForAnimationName);
            _platformTrigger.EnebelingConecters();
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_animator.IsInTransition(0));
        
        _isRotating = false;
    }
}