using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Agava.WebUtility;

public class FinishTrigger : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    private const string SaveNumberStarsName = "_currentStars";
    [SerializeField] private GameObject _canvasFinishMobile;
    [SerializeField] private GameObject _canvasGameOverMobile;
    [SerializeField] private GameObject _canvasFinish;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private GameObject _firstStarMobile;
    [SerializeField] private GameObject _secondStarMobile;
    [SerializeField] private GameObject _thirdStarMobile;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpawnBalls _spawn;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private string _levelName;
    private int _totalNumberStars = 0;
    [SerializeField] private float _currentAmountBalls = 0; // которые финишировали
    [SerializeField] private float _currentSpawnCount; // сколько есть мячей сейчас
    [SerializeField] private float _spawnCount; // начальное количество
    private float _currentPercent;
    private bool _isMobile;


    private void Start()
    {
        if (Device.IsMobile)
        {
            _canvasFinish = _canvasFinishMobile;
            _canvasGameOver = _canvasGameOverMobile;
            _firstStar = _firstStarMobile;
            _secondStar = _secondStarMobile;
            _thirdStar = _thirdStarMobile;
        }
        _spawnCount = _spawn.SpawnCount;
        _currentSpawnCount = _spawn.SpawnCount;
        PlayerPrefs.SetInt(_levelName, 0);
        if (PlayerPrefs.HasKey(SaveNumberStarsName))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStarsName);
        }
        PlayerPrefs.Save();
        Debug.Log(_levelName);
    }

    private void OnEnable()
    {
        Ground.BallOutGame += TakeAwayBall;
    }

    private void OnDisable()
    {
        Ground.BallOutGame -= TakeAwayBall;
    }

    private void Finish()
    {
        _currentPercent = _currentAmountBalls / _spawnCount * 100f;
        Debug.Log(_currentPercent.ToString());
        int totalStars = 0;

        switch (_currentPercent)
        {
            case >= 90f:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(true);
                _thirdStar.SetActive(true);
                ChargingStats();
                totalStars += 3;
                break;
            case >= 50f:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(true);
                ChargingStats();
                totalStars += 2;
                break;
            case > 30f:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                ChargingStats();                           
                _canvasFinish.SetActive(true);               
                totalStars++;
                break;
            case < 30f:               
                _canvasGameOver.SetActive(true);                                    
                break;

        }
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(_levelName, totalStars);
        PlayerPrefs.Save();
    }

    public void ChargingStats()
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
        if (_currentSpawnCount <= 0 || _currentSpawnCount == _currentAmountBalls)
        {
            Finish();
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
