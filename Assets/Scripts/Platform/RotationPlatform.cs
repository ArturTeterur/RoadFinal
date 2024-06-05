using Scripts.Platform.CheckingMovementsBalls;
using Scripts.Platform.StartingPosition;
using UnityEngine;

namespace Scripts.Platform.Rotation
{
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
        [SerializeField] private PlatformStartingPosition.PlatformPosition _platformStartingPosition;

        private string _currentAnimationTrigger;
        private bool _isRotating = false;

        private void Awake()
        {
            switch (_platformStartingPosition)
            {
                case PlatformStartingPosition.PlatformPosition.ZeroDegress:
                    _currentAnimationTrigger = _amimationZeroDegress;
                    break;
                case PlatformStartingPosition.PlatformPosition.NinetyDegress:
                    _currentAnimationTrigger = _amimationNinetyDegress;
                    break;
                case PlatformStartingPosition.PlatformPosition.OneHungreedAndEightyDegress:
                    _currentAnimationTrigger = _amimationOneHungreedAndEightyDegress;
                    break;
                case PlatformStartingPosition.PlatformPosition.TwoHundredAndSeventyDegress:
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
    }
}