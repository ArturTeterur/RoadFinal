using Agava.YandexGames;
using UnityEngine;
using TMPro;
using Agava.WebUtility;

public class FinishTrigger : MonoBehaviour
{
    private const string LeaderboardName = "LeaderBoard";
    private const string SaveNumberStars = "_currentStars";
    [SerializeField] private Ground _ground;
    [SerializeField] private GameObject _canvasGameOver;
    [SerializeField] private GameObject _canvasFinish;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private SpawnBalls _spawn;
    [SerializeField] private RectTransform _panelFinish;
    [SerializeField] private RectTransform _panelGameOver;
    [SerializeField] private float _currentAmountBalls = 0;
    [SerializeField] private float _currentSpawnCount;
    [SerializeField] private float _spawnCount;
    [SerializeField] private string _levelName;
    private int _totalNumberStars = 0;
    private float _currentPercent;
    private float _widthPanelMobile = 442.547f;
    private float _heightPanelMobile = 700f;
    private float _scaleXPanelMobile = 1.2f;
    private float _scaleYPanelMobile = 1.2f;
    private float _scaleZPanelMobile = 1f;

    private void Awake()
    {     
        if (Device.IsMobile)
        {
            _panelFinish.sizeDelta = new Vector2(_widthPanelMobile, _heightPanelMobile);
            _panelFinish.localScale = new Vector3(_scaleXPanelMobile, _scaleYPanelMobile, _scaleZPanelMobile);
            _panelGameOver.sizeDelta = new Vector2(_widthPanelMobile, _heightPanelMobile);
            _panelGameOver.localScale = new Vector3(_scaleXPanelMobile, _scaleYPanelMobile, _scaleZPanelMobile);
        }
    }

    private void Start()
    {
        _spawnCount = _spawn.SpawnCount;
        _currentSpawnCount = _spawn.SpawnCount;
        PlayerPrefs.SetInt(_levelName, 0);

        if (PlayerPrefs.HasKey(SaveNumberStars))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStars);
        }

        PlayerPrefs.Save();
    }

    private void OnEnable()
    {
        _ground.BallOutGame += TakeAwayBall;
    }

    private void OnDisable()
    {
        _ground.BallOutGame -= TakeAwayBall;
    }

    public void ChargingStats()
    {
        if (PlayerPrefs.HasKey(SaveNumberStars))
        {
            _totalNumberStars = PlayerPrefs.GetInt(SaveNumberStars);
            PlayerPrefs.SetInt(SaveNumberStars, _totalNumberStars++);
            PlayerPrefs.Save();
            if (PlayerAccount.IsAuthorized)
            {
                Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _totalNumberStars);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SaveNumberStars, _totalNumberStars++);
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
}
