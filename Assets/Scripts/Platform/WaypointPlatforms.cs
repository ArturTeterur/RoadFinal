using System.Collections.Generic;
using UnityEngine;

public class WaypointPlatforms : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private bool _itsStartPlatform;
    [SerializeField] private GameObject _secondTriggerPlatforms;
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            MoveToWaypoint(ballComponent);
            _secondTriggerPlatforms.SetActive(false);
        }
    }

    public void MoveToWaypoint(BallMovement ball)
    {
        ball.GetComponent<BallMovement>().KeepMoving();
        ball.GetComponent<BallMovement>().MoveToWaypoints(_waypoints);
    }
}
