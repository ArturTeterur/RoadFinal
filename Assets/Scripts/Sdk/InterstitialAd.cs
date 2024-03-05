using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

public class InterstitialAd : MonoBehaviour
{
    [SerializeField] private SoundMuteHandler _soundMuteHandler;

    private void OnEnable()
    {
        Application.focusChanged += ApplicationFocusChanged;
    }

    private void OnDisable()
    {
        Application.focusChanged -= ApplicationFocusChanged;
    }

    public void ShowAdv()
    {
        Agava.YandexGames.InterstitialAd.Show(Open, Close);
    }

    private void Close(bool close)
    {
        if (close)
        {
            _soundMuteHandler.OnVideoClosed();
        }
    }

    private void Open()
    {
        _soundMuteHandler.OnVideoOpened();
    }

    private void ApplicationFocusChanged(bool Focus)
    {
        if (Focus == true)
        {
            _soundMuteHandler.OnVideoOpened();
        }
    }
}
