using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private int _numberBalls;
    public float speed = 3f;
    [SerializeField] private List<Transform> _currentWaypoints;
    private int _currentWaypointIndex = 0;
    private Rigidbody _rigidbody;
    private FinishTrigger _finishTrigger;
    private float _minimalDistance = 0.6f;
    [SerializeField] bool _finished = false;
    [SerializeField] private BallMovement _targetBall;
    [SerializeField] private bool _obstacleAhead = false;
 
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _finishTrigger = FindObjectOfType<FinishTrigger>();
    }

    private void FixedUpdate()
    {        
         MoveToNextPoint();            
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
            if (ball._numberBalls == _numberBalls - 1)
            {
               _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
                _targetBall = ball;
                _obstacleAhead = true;
            }
        }
        if (collider.gameObject.TryGetComponent<FinishTrigger>(out FinishTrigger finish))
        {
            if (_finished == false)
            {
                finish.CountBalls();
                _finished = true;
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
             _rigidbody.constraints = RigidbodyConstraints.None;
              MoveToNextPoint();
            _obstacleAhead = false;
        }
    }


    public void MoveToNextPoint()
    {
        if (_currentWaypoints != null && _currentWaypointIndex < _currentWaypoints.Count)
        { 
            Vector3 moveDerection = (_currentWaypoints[_currentWaypointIndex].position - transform.position).normalized;       
           _rigidbody.velocity = moveDerection * speed;
             if (Vector3.Distance(transform.position, _currentWaypoints[_currentWaypointIndex].position) <= _minimalDistance)
             {
                _currentWaypointIndex++;
             }
             else
             {
                _rigidbody.AddForce(0, 0, -1 * speed);
             }
        }
    }

    public void Destroy()
    {
        _finishTrigger.TakeAwayBall();
        Destroy(gameObject);
    }

    public void MoveToWaypoints(List<Transform> waypoints) 
    {
        _currentWaypoints = waypoints;
        _currentWaypointIndex = 0;
        MoveToNextPoint();
    }

    public void KeepMoving()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;      
    }
     
    public int GetNumberBalls(int numberball)
    {
        _numberBalls = numberball;
        return numberball;      
    }
}
