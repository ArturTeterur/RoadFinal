using Agava.WebUtility;
using UnityEngine;

namespace Scripts.UI.LevelMenuMobile
{
    public class LevelsMenuMobile : MonoBehaviour
    {
        [SerializeField] private GameObject _CanvasMenu;
        [SerializeField] private GameObject _CanvasMenuMobile;

        private void Start()
        {
            if (Device.IsMobile)
            {
                _CanvasMenuMobile.SetActive(true);
            }
            else
            {
                _CanvasMenu.SetActive(true);
            }
        }
    }
}