using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuNextLevel : MonoBehaviour
{
    private const string CurrentLevel = "_currentLevel";
    private const string LeaderboardName = "LeaderBoard";
    [SerializeField] private SpawnBalls _spawnBalls;
    private int _nextLevel;
    private int _menuNumber = 1;
    private int _currentLevel;
    private int _lastLevel = 21;
    private int _levelAfterLast = 15;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(CurrentLevel))
        {
            _currentLevel = PlayerPrefs.GetInt(CurrentLevel);          
        }
        else
        {
            _currentLevel = 1;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex);       
    }

    public void StartLevel(GameObject canvasStart)
    {
        Time.timeScale = 1;
        canvasStart.SetActive(false);
        _spawnBalls.StartLevel();
    }

    public void NextLevel()
    {
        _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (_nextLevel == _lastLevel)
        {
            _nextLevel = _levelAfterLast;
        }
        SceneManager.LoadScene(_nextLevel);
        PlayerPrefs.SetInt(CurrentLevel, SceneManager.GetActiveScene().buildIndex);
        if (PlayerAccount.IsAuthorized)
        {
            Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _currentLevel++);
        }
    }

    public void MainMenuLevel()
    {
        if (PlayerPrefs.HasKey(CurrentLevel))
        {
            int level = PlayerPrefs.GetInt(CurrentLevel);
            SceneManager.LoadScene(level);
        }
        else
        {
            _nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(_nextLevel);
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(_menuNumber);
    }

    public void GoLevel(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }
}
