using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _button;

    private void Start()
    {
        ForwardMovement.speed = 0;
        _button.onClick.AddListener(TryAgain);
    }

    private void TryAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
