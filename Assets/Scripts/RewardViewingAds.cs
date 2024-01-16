using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Agava.YandexGames;
using System;
using UnityEngine.UI;

public class RewardViewingAds : MonoBehaviour
{
    [SerializeField] private GameObject _buttonShowAdv;
    [SerializeField] private SoundMuteHandler _soundMuteHandler;
    [SerializeField] private GameObject _soundButton;
    [SerializeField] private Button _buttonAd;
    [SerializeField] private PlatformMovement _platformMovement;
    [SerializeField] private MenuNextLevel _menuNextLevel;

    public void ShowAdvButton()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenVideo, OnRewarded, OnClose, OnError);
    }

    private void OnError(string obj)
    {
        _soundMuteHandler.OnVideoOpened();
        Time.timeScale = 0;

        if (Input.GetKeyDown("space"))
        {
            _soundMuteHandler.OnVideoOpened();
        }

        if (obj != null)
        {
            _soundMuteHandler.OnVideoOpened();
        }
    }

    private void OnOpenVideo()
    {
        Time.timeScale = 0;
        _soundMuteHandler.OnVideoOpened();
        _buttonAd.interactable = false;
        _buttonShowAdv.SetActive(false);
    }

    private void OnRewarded()
    {
        Time.timeScale = 0;
        if (Input.GetKeyDown("space"))
        {
            _soundMuteHandler.OnVideoOpened();
        }
        
    }

    private void OnClose()
    {
        _menuNextLevel.NextLevel();
        Time.timeScale = 1;
        _soundMuteHandler.OnVideoClosed();
    }
}
