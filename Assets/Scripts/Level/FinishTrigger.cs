using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Localization;
using System.Linq;
using Unity.VisualScripting;

public class FinishTrigger : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    private const string SaveNumberStarsName = "_currentStars";
    [SerializeField] private GameObject _canvasFinish;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpawnBalls _spawn;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private string _levelName;
    [SerializeField] private InterstitialAd _interstitialAd;
    private int _totalNumberStars = 0;
    private float _currentAmountBalls = 0;
    private float _currentSpawnCount;
    private float _spawnCount;
    private float _currentPercent;

    private void Start()
    {
        _spawnCount = _spawn.SpawnCount;
        _currentSpawnCount = _spawn.SpawnCount;
        if (PlayerPrefs.HasKey(SaveNumberStarsName))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStarsName);
        }
    }

    private void OnEnable()
    {
        MoveOnPlatformTrigger._removingBallWhenTurning += RemovalFromTotalBoals;
        Ground.BallOutGame += TakeAwayBall;
    }

    private void OnDisable()
    {
        MoveOnPlatformTrigger._removingBallWhenTurning -= RemovalFromTotalBoals;
        Ground.BallOutGame -= TakeAwayBall;
    }

    private void Finish()
    {
        _currentPercent = _currentAmountBalls / _spawnCount * 100f;
        Debug.Log(_currentPercent.ToString());
        int totalStars = 0;

        switch (_currentPercent)
        {
            case >= 90:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(true);
                _thirdStar.SetActive(true);
                ChargingStats();
                totalStars++;
                break;
            case >= 50:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(true);
                ChargingStats();
                totalStars++;
                break;
            case >30:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                ChargingStats();
                totalStars++;
                break;
            case < 30:
                _canvasGameOver.SetActive(true);
                break;

        }
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(_levelName, totalStars);
        PlayerPrefs.Save();
        Debug.Log(totalStars.ToString() + " star");
        _interstitialAd.ShowAdv();
    }

    private void RemovalFromTotalBoals()
    {
        _spawnCount--;
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

    public void TakeAwayBall()
    {
        _currentSpawnCount--;
        if (_currentSpawnCount <= 0)
        {
            _canvasGameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void CountBalls()
    {
        _currentAmountBalls++;
        _text.text = _currentAmountBalls.ToString();
        if (_currentSpawnCount == _currentAmountBalls)
        {
            Finish();
        }
    }
}
