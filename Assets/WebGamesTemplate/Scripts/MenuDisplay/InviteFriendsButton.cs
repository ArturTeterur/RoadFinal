
using System;
using UnityEngine;
using UnityEngine.UI;

public class InviteFriendsButton : MonoBehaviour
{
    [SerializeField] private Button _button;

#if YANDEX_GAMES
    private void Awake()
    {
        gameObject.SetActive(false);
    }
#endif

    private void OnRewardedCallback()
    {
        //Reward the player with in-game money
    }
}
