using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

public class MainMenu : MonoBehaviour
{
    private const string CurrentLevel = "_currentLevel";
    [SerializeField] private int _numberIndexLevelSelection;
    private int _nextLevel;

    private void Start()
    {
        Time.timeScale = 0.0f;
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

    public void GoLevelSelection()
    {
        SceneManager.LoadScene(_numberIndexLevelSelection);
    }
}
