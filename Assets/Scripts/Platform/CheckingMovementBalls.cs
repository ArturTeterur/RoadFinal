using System.Collections.Generic;
using UnityEngine;

public class CheckingMovementBalls : MonoBehaviour
{
    [SerializeField] private List<BallMovement> _objectsOnPlatform = new List<BallMovement>();
    [SerializeField] private List<GameObject> _connectersPlatform;
    [SerializeField] private bool _straightPlatform;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            ballComponent.BallOnPlatform();
            _objectsOnPlatform.Add(ballComponent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            ballComponent.BallOnPlatform();
            _objectsOnPlatform.Remove(ballComponent);
        }
    }

    public void EnebelingConecters()
    {
        if (_connectersPlatform[0].activeSelf == true)
        {
            _connectersPlatform[0].SetActive(false);
            _connectersPlatform[1].SetActive(true);
        }
        else if (_connectersPlatform[1].activeSelf == true)
        {
            _connectersPlatform[1].SetActive(false);
            _connectersPlatform[0].SetActive(true);
        }

        ChangeDerection();
    }

    private void ChangeDerection()
    {
        for (int i = 0; i < _objectsOnPlatform.Count; i++)
        {
            _objectsOnPlatform[i].ChangeDerection();
        }
    }
}