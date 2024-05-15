using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MoveOnPlatformTrigger : MonoBehaviour
{
    [SerializeField] private List<BallMovement> objectsOnPlatform = new List<BallMovement>();
    [SerializeField] private RotationPlatform _rotationPlatform;
    [SerializeField] private List<GameObject> _connectersPlatform;
    [SerializeField] private GameObject _directionMovementAfterTurning;
    [SerializeField] private bool _straightPlatform;
    private SpawnBalls _spawnBalls;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            ballComponent.BallOnPlatform();
            objectsOnPlatform.Add(ballComponent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            ballComponent.BallOnPlatform();
            objectsOnPlatform.Remove(ballComponent);
        }
    }

    private void ChangeDerection()
    {
        for (int i = 0; i < objectsOnPlatform.Count; i++)
        {
            objectsOnPlatform[i].ChangeDerection();
        }
    }

    public void EnebelingConecters()
    {
        if (_connectersPlatform[0].activeSelf == true)
        {
            _connectersPlatform[0].SetActive(false);
            _connectersPlatform[1].SetActive(true);
            Debug.Log("Connecter 1");
        }
        else if (_connectersPlatform[1].activeSelf == true)
        {
            _connectersPlatform[1].SetActive(false);
            _connectersPlatform[0].SetActive(true);
            Debug.Log("Connecter 2");
        }

        ChangeDerection();   
    }
}