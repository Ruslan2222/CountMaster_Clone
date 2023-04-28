using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    public GameObject endGamePanel => _panel;

    [SerializeField] private TextMeshProUGUI _titleText;
    [Header("Coins")]
    [Space]
    [SerializeField] private GameObject _coins;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _earnedCoinText;
    [Header("Diamonds")]
    [Space]
    [SerializeField] private GameObject _diamonds;
    [SerializeField] private TextMeshProUGUI _diamondsText;
    [SerializeField] private TextMeshProUGUI _earnedDiamondsText;
    [Space]
    [SerializeField] private Button _nextLevelButton;

    private void Start()
    {
        Open();
    }

    private void Open()
    {
        string levelType = Level.levelType;

        if (levelType == "Multi")
        {
            Multiplier();
        }
        else if (levelType == "Boss")
        {
            BossDefeat();
        }
    }

    private void BossDefeat()
    {
        _titleText.text = "Boss Defeat";
        _earnedDiamondsText.text = "20";
        _diamonds.SetActive(true);
        _nextLevelButton.onClick.AddListener(BonusGame);
    }

    private void Multiplier()
    {
        float levelCoins = PlayerPrefs.GetFloat("levelCoins");
        _titleText.text = "Level Completed";
        _coins.SetActive(true);
        _coinText.text = $"{(int)levelCoins}";
        _nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void BonusGame()
    {
        int diamonds = PlayerPrefs.GetInt("diamonds");
        int level = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("diamonds", diamonds += 1);
        PlayerPrefs.SetInt("level", level++);
        SceneManager.LoadScene("Game");
    }

    private void NextLevel()
    {
        float levelCoins = PlayerPrefs.GetFloat("levelCoins");
        int level = PlayerPrefs.GetInt("level");
        int coins = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", coins += (int)levelCoins);
        level++;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene("Game");
    }

}
