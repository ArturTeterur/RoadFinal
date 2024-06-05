using Lean.Localization;
using UnityEngine;

namespace Scripts.UI.Language
{
    public class Language : MonoBehaviour
    {
        private const string CurrentLanguage = "_currentLanguage";
        private const string RussianLanguage = "Russian";
        private const string TurkishLanguage = "Turkish";
        private const string EnglishLanguage = "English";

        [SerializeField] private LeanLocalization _leanLocalization;

        private string _language;

        private void Start()
        {
            LoadLocalization();
        }

        private void LoadLocalization()
        {
            _language = PlayerPrefs.GetString(CurrentLanguage);
            switch (_language)
            {
                case "ru":
                    _leanLocalization.SetCurrentLanguage(RussianLanguage);
                    break;
                case "tr":
                    _leanLocalization.SetCurrentLanguage(TurkishLanguage);
                    break;
                case "en":
                    _leanLocalization.SetCurrentLanguage(EnglishLanguage);
                    break;
                default:
                    _leanLocalization.SetCurrentLanguage(RussianLanguage);
                    break;
            }
        }
    }
}