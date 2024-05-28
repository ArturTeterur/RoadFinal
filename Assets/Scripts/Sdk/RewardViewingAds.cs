using Scripts.Platform.PlatformMovement;
using Scripts.UI.MenuNextLevel;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Sdk.RewardAds
{
    public class RewardViewingAds : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonShowAdv;
        [SerializeField] private SoundMuteHandler _soundMuteHandler;
        [SerializeField] private GameObject _soundButton;
        [SerializeField] private Button _buttonAd;
        [SerializeField] private PlatformMovement _platformMovement;
        [SerializeField] private MenuNextLevel _menuNextLevel;
        [SerializeField] private GameObject _testFocus;
        [SerializeField] private string _nameNextLevel;

        private void Start()
        {
            if (_soundMuteHandler = null)
            {
                _soundMuteHandler = GameObject.FindObjectOfType<SoundMuteHandler>();
            }
        }

        public void ShowAdvButton()
        {
            Agava.YandexGames.VideoAd.Show(OnOpenVideo, OnRewarded, OnClose, OnError);
        }

        private void OnError(string obj)
        {
            _soundMuteHandler.OnVideoOpened();
            Time.timeScale = 0;

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
            _testFocus.SetActive(false);
        }

        private void OnRewarded()
        {
            if (_nameNextLevel != null)
            {
                PlayerPrefs.SetInt(_nameNextLevel, 0);
            }

            Time.timeScale = 0;
        }

        private void OnClose()
        {
            _menuNextLevel.NextLevel();
            Time.timeScale = 1;
            _soundMuteHandler.OnVideoClosed();
            _testFocus.SetActive(true);
        }
    }
}