using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using Agava.WebUtility;

public class Init : MonoBehaviour
{
    private const string CurrentLanguage = "_currentLanguage";
    [SerializeField] private GameObject _buttonStart;
    [SerializeField] private GameObject _loadingBar;
    private int _menuSceneNumber = 1;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return Agava.YandexGames.YandexGamesSdk.Initialize(OnInitialized);
    }
    public void OnCallGameReadyButtonClick()
    {
        SceneManager.LoadScene(_menuSceneNumber);
    }

    private void OnInitialized()
    {
        PlayerPrefs.SetString(CurrentLanguage, YandexGamesSdk.Environment.i18n.lang);
        _loadingBar.SetActive(false);
        YandexGamesSdk.GameReady();
        _buttonStart.SetActive(true);
    }
}
