using Scripts.Level.Spawn;
using Scripts.Sdk.InterstitialAds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MenuNextLevel
{
    public class MenuNextLevel : MonoBehaviour
    {
        private const string CurrentLevel = "_currentLevel";
        private const string LeaderboardName = "LeaderBoard";

        [SerializeField] private SpawnBalls _spawnBalls;
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private bool _educationLevel;
        [SerializeField] private GameObject _education;

        private int _nextLevel;
        private int _menuNumber = 1;
        private int _lastLevel = 21;
        private int _levelAfterLast = 15;

        private void Awake()
        {
            PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex);
        }

        private void Start()
        {
            if (!_educationLevel)
            {
                StartLevel();
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartLevel()
        {
            if (_educationLevel)
            {
                Time.timeScale = 1;
                _educationLevel = false;

                if (_education != null)
                {
                    _education.SetActive(false);
                }

                _spawnBalls.StartLevel();
            }
            else
            {
                Time.timeScale = 1;
                _spawnBalls.StartLevel();
            }
        }

        public void NextLevel()
        {
            _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (_nextLevel == _lastLevel)
            {
                _nextLevel = _levelAfterLast;
            }

            SceneManager.LoadScene(_nextLevel);
        }

        public void Menu(bool winnerMenu)
        {
            SceneManager.LoadScene(_menuNumber);

            if (winnerMenu)
            {
                PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}