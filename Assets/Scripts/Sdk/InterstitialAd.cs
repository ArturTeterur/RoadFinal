using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class InterstitialAd : MonoBehaviour
{
    [SerializeField] private SoundMuteHandler _soundMuteHandler;
    [SerializeField] private GameObject _testFocus;

    private void Start()
    {
        ShowAdv();
        if(_soundMuteHandler = null)
        {
            _soundMuteHandler = GameObject.FindObjectOfType<SoundMuteHandler>();
        }
    }
    private void ShowAdv()
    {
        Agava.YandexGames.InterstitialAd.Show(Open, Close);
    }

    private void Close(bool close)
    {
        if (close)
        {
            Time.timeScale = 1;
            _soundMuteHandler.OnVideoClosed();
            _testFocus.SetActive(true);
        }
    }

    private void Open()
    {
        Time.timeScale = 0;
        _soundMuteHandler.OnVideoOpened();
        _testFocus.SetActive(false);
    }
}
