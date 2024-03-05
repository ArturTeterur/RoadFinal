using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private string _nameLevel;
    [SerializeField] private GameObject _firstStar;
    [SerializeField] private GameObject _secondStar;
    [SerializeField] private GameObject _thirdStar;
    [SerializeField] private GameObject _closebutton;
    [SerializeField] private int _menuNumber;

    private void Start()
    {
        Debug.Log(_nameLevel);
        Debug.Log(_nameLevel + PlayerPrefs.GetInt(_nameLevel));
        if(PlayerPrefs.HasKey(_nameLevel))
        {
            switch (PlayerPrefs.GetInt(_nameLevel))
            {
                case 1:
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
