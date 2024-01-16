using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private LeaderBoardButton _leaderBoardButton;

    private void Start()
    {
        Show();
    }

    public void Show()
    {
        _leaderBoardButton.gameObject.SetActive(true);
    }
    public void Hide()
    {
        _leaderBoardButton.gameObject.SetActive(false);
    }
}
