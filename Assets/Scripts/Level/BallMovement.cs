using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _currentWaypoints;
    [SerializeField] private int _numberBalls;
    [SerializeField] float _speed = 3f;
    [SerializeField] private float _maxSpeed;
    [SerializeField] bool _finished = false;
    [SerializeField] private BallMovement _targetBall;
    [SerializeField]private int _currentWaypointIndex = 0;
    [SerializeField] GameObject _finishEffect;
    private float _minimalDistance = 1f;
    private Rigidbody _rigidbody;
    private FinishTrigger _finishTrigger;
 
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _finishTrigger = FindObjectOfType<FinishTrigger>();
    }

    private void FixedUpdate()
    {
        Move();     
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
            if (ball._numberBalls == _numberBalls - 1)
            {
               _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
                _targetBall = ball;
            }
        }
        if (collider.gameObject.TryGetComponent<FinishTrigger>(out FinishTrigger finish))
        {
            if (_finished == false)
            {
                finish.CountBalls();
                _finished = true;
                Destroy(gameObject);
                Instantiate(_finishEffect, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
             _rigidbody.constraints = RigidbodyConstraints.None;
              Move();
            if (_targetBall == null)
            {
                Move();
            }
        }
    }

    public void Move()
    {
        if (_currentWaypoints != null && _currentWaypointIndex < _currentWaypoints.Count)
        {
            Vector3 moveDerection = (_currentWaypoints[_currentWaypointIndex].position - transform.position).normalized;
            if (_rigidbody.velocity.magnitude < _maxSpeed)
            {
                _rigidbody.AddForce(moveDerection * _speed, ForceMode.Impulse);     
            }
            if (Vector3.Distance(transform.position, _currentWaypoints[_currentWaypointIndex].position) <= _minimalDistance)
            {
                _currentWaypointIndex++;
            }
        }
        else
        {
            if (_rigidbody.velocity.magnitude < _maxSpeed)
            {
                _rigidbody.AddForce(Vector3.forward * _speed * -1, ForceMode.Impulse);
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
        Move();
    }

    public void KeepMoving()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;      
    }

    public void StopMoving()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    }
     
    public int GetNumberBalls(int numberball)
    {
        _numberBalls = numberball;
        return numberball;      
    }
}
