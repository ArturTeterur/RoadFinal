using Agava.YandexGames;
using UnityEngine;

namespace Scripts.Sdk.AutarizationWindow
{
    public class AutarizationWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _autorizationWindow;

        public void CloseWindowAutorization()
        {
            _autorizationWindow.gameObject.SetActive(false);
        }

        public void Autorization()
        {
            PlayerAccount.Authorize();
        }
    }
}