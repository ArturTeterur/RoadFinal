using UnityEngine;
using System;
using UnityEngine.Events;

public class Ground : MonoBehaviour
{
   [SerializeField] private UnityEvent _ballFell;

    public event UnityAction BallOutGame
    {
        add => _ballFell.AddListener(value);
        remove => _ballFell.RemoveListener(value);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            ballComponent.Destroy();
            _ballFell.Invoke();
        }
    }
}
