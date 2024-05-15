using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _CanvasMenu;
    [SerializeField] private GameObject _CanvasMenuMobile;
    [SerializeField] private string _nameLevel;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private GameObject _closebutton;
    [SerializeField] private GameObject _nextLevelButton;
    [SerializeField] private int _menuNumber;

    private void Start()
    {
        if(PlayerPrefs.HasKey(_nameLevel))
        {
            switch (PlayerPrefs.GetInt(_nameLevel))
            {
                case 1:
                    if (_nextLevelButton != null)
                    {
                        _nextLevelButton.SetActive(false);
                    }
                    _firstStar.SetActive(true);
                    break;
                case 2:
                    _firstStar.SetActive(true);
                    _secondStar.SetActive(true);
                    break;
                case 3:
                    _firstStar.SetActive(true);
                    _secondStar.SetActive(true);
                    _thirdStar.SetActive(true);
                    break;
            }
            _closebutton.SetActive(false);
        }
    }

    public void GoLevel(int numberScene)
    {
        SceneManager.LoadScene(numberScene);
    }  
}
