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
    [SerializeField] GameObject _canvasFinish;
    [SerializeField] GameObject _firstStar;
    [SerializeField] GameObject _secondStar;
    [SerializeField] GameObject _thirdStar;
    [SerializeField] private SpawnBalls _spawn;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private string _levelName;
    [SerializeField] private InterstitialAd _interstitialAd;
    [SerializeField] private List<RotationPlatform> _rotationPlatforms;
    private bool _allCorrectRotation = false;
    private int _totalNumberStars = 0;
    private float _currentAmountBalls = 0;
    private float _fallenBalls = 0;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveNumberStarsName))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStarsName);
        }
    }

    private void Finish()
    {
        int totalStars = 0;

        switch (_fallenBalls)
        {
            case < 2:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(true);
                _thirdStar.SetActive(true);
                totalStars += 3;
                break;
            case <5:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(false);
                _thirdStar.SetActive(false);
                ChargingStats();
                totalStars+= 2;
                break;
            case >10:
                _canvasFinish.SetActive(true);
                _firstStar.SetActive(true);
                _secondStar.SetActive(false);
                _thirdStar.SetActive(false);
                ChargingStats();
                totalStars++;
                break;
        }
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(_levelName, totalStars);
        PlayerPrefs.Save();
        _interstitialAd.ShowAdv();
    }

    private void CheckingAllPlatforms()
    {
        int numberCorrectPlatform = 0;
        for (int i = 0; i < _rotationPlatforms.Count; i++)
        {
            if (_rotationPlatforms[i].CorrectPositionPlatform == true)
            {
                numberCorrectPlatform++;
            }          
        }

        if(numberCorrectPlatform == _rotationPlatforms.Count)
        {
            _allCorrectRotation = true;
            Finish();
        }
    }

    private void ChargingStats()
    {
        if (PlayerPrefs.HasKey(SaveNumberStarsName))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStarsName);
            PlayerPrefs.SetInt(SaveNumberStarsName, _totalNumberStars++);
            PlayerPrefs.Save();
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _totalNumberStars);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SaveNumberStarsName, _totalNumberStars++);
            PlayerPrefs.Save();
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _totalNumberStars);
            }
        }
    }
    public void CountBalls()
    {
        _currentAmountBalls++;
       if (_currentAmountBalls >= 10)
       {
            CheckingAllPlatforms();
       }
    }

    public void TakeAwayBall()
    {
        _fallenBalls++;
    }  
}
