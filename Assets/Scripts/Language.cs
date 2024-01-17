using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;
using Agava.YandexGames;
using Lean.Localization;

public class Language : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;
    private string _language;

    private void Start()
    {
        LoadLocalization();
    }

    private void LoadLocalization()
    {
        _language = PlayerPrefs.GetString("_currentLanguage");
        switch (_language)
        {
            case "ru":
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
            case "tr":
                _leanLocalization.SetCurrentLanguage("Turkish");
                break;
            case "en":
                _leanLocalization.SetCurrentLanguage("English");
                break;
            default:
                _leanLocalization.SetCurrentLanguage("Russian");
                break;
        }
    }
}
