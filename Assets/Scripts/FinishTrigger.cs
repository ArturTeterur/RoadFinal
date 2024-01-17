using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    [SerializeField] GameObject _canvasFinish;
    [SerializeField] GameObject _firstStar;
    [SerializeField] GameObject _secondStar;
    [SerializeField] GameObject _thirdStar;
    [SerializeField] private SpawnBalls _spawn;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private string _levelName;
    private int _totalNumberStars = 0;
    private float _currentAmountBalls = 0;
    private float _spawnCount;
    private float _currentPercent;
    private float _currentSpawnCount;

    private void Start()
    {
        if (PlayerPrefs.HasKey("_currentStars"))
        {
            _totalNumberStars = PlayerPrefs.GetInt("_currentStars");
        }

        _spawnCount = _spawn.SpawnCount;       
        _currentSpawnCount = _spawn.SpawnCount;
    }

    private void Finish()
    {
        _currentPercent = _currentAmountBalls / _spawnCount * 100f;
        Debug.Log(_currentPercent.ToString());
        int totalStars = 0;
        
        if (_currentPercent < 30)
        {
            _canvasGameOver.SetActive(true);
        }
        if (_currentPercent >= 30)
        {
            _canvasFinish.SetActive(true);
            _firstStar.SetActive(true);
            ChargingStats();
            totalStars++;
        }
        if (_currentPercent >= 60)
        {
            _secondStar.SetActive(true);
            ChargingStats();
            totalStars++;

        }
        if (_currentPercent >= 90)
        {
            _thirdStar.SetActive(true);
            ChargingStats();
            totalStars++;

        }
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(_levelName, totalStars);
    }

    public void CountBalls()
    {
        _currentAmountBalls++;
        if (_currentSpawnCount == _currentAmountBalls)
        {
            Finish();
        }
    }

   

    public void TakeAwayBall()
    {
        _currentSpawnCount--;
        if (_currentSpawnCount <= 0)
        {
            Time.timeScale = 0;
        }
    }
    
    private void ChargingStats()
    {
        if (PlayerPrefs.HasKey("_currentStars"))
        {
            _totalNumberStars = PlayerPrefs.GetInt("_currentStars");
            PlayerPrefs.SetInt("_currentStars", _totalNumberStars++);
            PlayerPrefs.Save();
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _totalNumberStars);
            }
        }
        else
        {
            PlayerPrefs.SetInt("_CurrentStars", _totalNumberStars++);
            PlayerPrefs.Save();
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _totalNumberStars);
            }
        }
    }
}
