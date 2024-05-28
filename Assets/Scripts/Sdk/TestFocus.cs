using Agava.WebUtility;
using UnityEngine;

namespace Scripts.Sdk.Focus
{
    public class TestFocus : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundMuteHandler _soundMuteHandler;

        private bool _openAd = false;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgraundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgraundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgraundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgraundChangeWeb;
        }

        public void MuteAudioDuring()
        {
            _openAd = true;
        }

        public void TurnSound()
        {
            _openAd = false;
        }

        private void OnInBackgraundChangeApp(bool inApp)
        {
            PauseGame(!inApp);
        }

        private void OnInBackgraundChangeWeb(bool isBackground)
        {
            MuteAudio(isBackground);
            PauseGame(isBackground);
        }

        private void MuteAudio(bool value)
        {
            _soundMuteHandler.SoundMuteButtonOn();
            if (!_openAd)
            {
                if (value)
                {
                    _soundMuteHandler.DisableSound();
                }
                else
                {
                    _soundMuteHandler.EnableSound();
                }
            }
        }

        private void PauseGame(bool value)
        {
            if (!_openAd)
            {
                Time.timeScale = value ? 0 : 1;
            }
        }
    }
}