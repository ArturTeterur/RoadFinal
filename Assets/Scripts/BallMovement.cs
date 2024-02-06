using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private int _numberBalls;
    [SerializeField] float _speed = 3f;
    [SerializeField] private List<Transform> _currentWaypoints;
    [SerializeField] bool _finished = false;
    [SerializeField] private BallMovement _targetBall;
    [SerializeField] private bool _obstacleAhead = false;
    private int _currentWaypointIndex = 0;
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
                _targetBall = ball;            }
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
              Move();
        }
    }

    public void Move()
    {
        Vector3 movement = new Vector3(0, 0, -1);
        _rigidbody.velocity = movement * _speed;
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
     
    public int GetNumberBalls(int numberball)
    {
        _numberBalls = numberball;
        return numberball;      
    }
}
