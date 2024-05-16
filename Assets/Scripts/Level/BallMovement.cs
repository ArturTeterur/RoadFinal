using System;
using System.Collections.Generic;
using UnityEngine;
public class BallMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _currentWaypoints;
    [SerializeField] int _numberBalls;
    [SerializeField] float _speed = 3f;
    [SerializeField] private float _maxSpeed;
    [SerializeField] bool _finished = false;
    [SerializeField] private BallMovement _targetBall;
    [SerializeField] private int _currentWaypointIndex = 0;
    [SerializeField] GameObject _finishEffect;
    [SerializeField] private bool _changeDirection = false;
   [SerializeField] private bool _onPlatform;
    private float _minimalDistance = 1f;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.drag = 0f;      
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
            if (ball._numberBalls > _numberBalls)
            {
                GetIndexWaypoint();
            }
        }
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
            if(ball._numberBalls < _numberBalls)
            {
                if (_changeDirection == true && ball._changeDirection == false)
                {
                    KeepMoving();
                    ChangeDerection();
                }
                
            }
        }
        if (collider.gameObject.TryGetComponent<FinishTrigger>(out FinishTrigger finish))
        {
            if (_finished == false)
            {
                _finished = true;
                Destroy(gameObject);
                Instantiate(_finishEffect, transform.position, Quaternion.identity);
                finish.CountBalls();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ball))
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            if (_targetBall == null)
            {
                Move();
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void ChangeDerection()
    {
        if (!_changeDirection)
        {
            _changeDirection = true;
        }
        else
        {
            _changeDirection = false;
        }
        Move();
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
        if (_onPlatform)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            KeepMoving();
        }
    }

    public int GetNumberBalls(int numberball)
    {
        _numberBalls = numberball;
        return numberball;
    }

    public void BallOnPlatform()
    {
        if (_onPlatform)
        {
            _onPlatform = false;
        }
        else
        {
            _onPlatform = true;
        }
    }

    private void Move()
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
                GetIndexWaypoint();
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

    private int GetIndexWaypoint()
    {
        if (_changeDirection)
        {
            _currentWaypointIndex--;
            if (_currentWaypointIndex < 0)
            {
                _changeDirection = false;
                _currentWaypointIndex = 0;
                return _currentWaypointIndex;
            }
            else
            {
                return _currentWaypointIndex;
            }
        }
        else
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex > _currentWaypoints.Count)
            {
                _currentWaypointIndex = _currentWaypoints.Count;
                return _currentWaypointIndex;
            }
            else
            {
                return _currentWaypointIndex;
            }
        }
    }
}