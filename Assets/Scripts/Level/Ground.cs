using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ground : MonoBehaviour
{
    public static event Action BallOutGame;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            BallOutGame();
            ballComponent.Destroy();
        }
    }
}
