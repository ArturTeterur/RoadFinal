using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveOnPlatformTrigger : MonoBehaviour
{
    [SerializeField] private List<BallMovement> objectsOnPlatform = new List<BallMovement>();
    [SerializeField] private RotationPlatform _rotationPlatform;
    [SerializeField] private List<GameObject> _connectersPlatform;
    private bool _ballOnPlatform;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            objectsOnPlatform.Add(ballComponent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            objectsOnPlatform.Remove(ballComponent);
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
        if (objectsOnPlatform.Count >0)
        {
            for (int i = 0; i < objectsOnPlatform.Count; i++)
            {
                objectsOnPlatform[i].DestroyOnPlatform();
                objectsOnPlatform.Remove(objectsOnPlatform[i]);             
            }
        }
        
        EneblingConnecters();        
    }
}
