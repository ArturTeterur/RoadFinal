using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveOnPlatformTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> objectsOnPlatform = new List<GameObject>();
    [SerializeField] private RotationPlatform _rotationPlatform;
    [SerializeField] private List<GameObject> _connectersPlatform;
    public static event Action _removingBallWhenTurning;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            objectsOnPlatform.Add(ballComponent.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            objectsOnPlatform.Remove(ballComponent.gameObject);
        }
    }
    private void EneblingConnecters()
    {
        for (int i = 0; i < _connectersPlatform.Count; i++)
        {
            _connectersPlatform[i].SetActive(true);
        }
    }

    public void DestroyObjectsOnPlatform()
    {
        foreach (GameObject obj in objectsOnPlatform)
        {
            Destroy(obj);
            _removingBallWhenTurning();
        }
        EneblingConnecters();
    }
}
