using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBalls : MonoBehaviour
{
    private const string CoroutineName = "BallCreation";
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private BallMovement _ballPrefab;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private int _currentNumberBall = 0;
    [SerializeField] private List<BallMovement> _balls = new List<BallMovement>();
    [SerializeField] private PlatformMovement _platformMovement;
    [SerializeField] private bool _ballInWay = false;
    private float _spawnCount;


    private IEnumerator BallCreation()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            if (!_ballInWay)
            {
                BallMovement ball = Instantiate(_balls[Random.Range(0, _balls.Count)], _spawnPoint.transform.position, Quaternion.identity);           
                int numberball = _currentNumberBall;
                ball.GetComponent<BallMovement>().GetNumberBalls(i);
                yield return new WaitForSeconds(_timeSpawn);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<BallMovement>(out BallMovement ballComponent))
        {
            _ballInWay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _ballInWay = false;
    }

    public void StartLevel()
    {
        _spawnCount = Mathf.Infinity;
        _platformMovement.GameStart();
        if (!_ballInWay)
        {
            StartCoroutine(CoroutineName);
        }
    }
}
