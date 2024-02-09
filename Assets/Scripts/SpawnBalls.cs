using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBalls : MonoBehaviour
{
    private const string CoroutineName = "WaterCreation";
    [SerializeField] private int _spawnCount;
    public int SpawnCount => _spawnCount;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private BallMovement _ballPrefab;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private int _currentNumberBall = 0;
    [SerializeField] private List<BallMovement> _balls = new List<BallMovement>();
    [SerializeField] private PlatformMovement _platformMovement;

    public void StartLevel()
    {
        StartCoroutine(CoroutineName);
        _platformMovement.GameStart();
    }

    private IEnumerator WaterCreation()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            BallMovement ball = Instantiate(_balls[Random.Range(0, _balls.Count)], _spawnPoint.transform.position, Quaternion.identity);           
            int numberball = _currentNumberBall;
            ball.GetComponent<BallMovement>().GetNumberBalls(i);
            yield return new WaitForSeconds(_timeSpawn);
        }
    }
}
