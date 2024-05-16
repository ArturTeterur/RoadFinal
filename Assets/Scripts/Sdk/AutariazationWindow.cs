using UnityEngine;
using Agava.YandexGames;

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
