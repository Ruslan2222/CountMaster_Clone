using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Panel")]
    [Space]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _diamondsText;
    [SerializeField] private Button _colorsButton;
    [SerializeField] private Button _skinsButton;
    [Header("Start Units")]
    [Space]
    [SerializeField] private Button _startUnitsButton;
    [SerializeField] private TextMeshProUGUI _levelUnitText;
    [SerializeField] private TextMeshProUGUI _priceUnitText;
    [Header("Income")]
    [Space]
    [SerializeField] private Button _incomeButton;
    [SerializeField] private TextMeshProUGUI _levelIncomeText;
    [SerializeField] private TextMeshProUGUI _priceIncomeText;
    [Header("Win Game Panel")]
    [Space]
    [SerializeField] private GameObject _winPanel;
    [Header("Lose Game Panel")]
    [Space]
    [SerializeField] private GameObject _losePanel;
    [Space]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshPro _unitCounter;

    private int _coins, _diamonds, _level;
    private int _levelUnit, _priceUnit;
    private int _levelIncome, _priceIncome;

    private void Awake()
    {
        GetData();
    }

    private void GetData()
    {

        #region PlayerPrefs

        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("coins", 0);
            PlayerPrefs.SetInt("diamonds", 0);
            PlayerPrefs.SetInt("levelUnit", 1);
            PlayerPrefs.SetInt("priceUnit", 100);
            PlayerPrefs.SetInt("levelIncome", 1);
            PlayerPrefs.SetInt("priceIncome", 100);
        }

        _level = PlayerPrefs.GetInt("level");
        _coins = PlayerPrefs.GetInt("coins");
        _diamonds = PlayerPrefs.GetInt("diamonds");

        _levelUnit = PlayerPrefs.GetInt("levelUnit");
        _priceUnit = PlayerPrefs.GetInt("priceUnit");

        _levelIncome = PlayerPrefs.GetInt("levelIncome");
        _priceIncome = PlayerPrefs.GetInt("priceIncome");
        #endregion

        #region Text
        _levelText.text = $"level {_level}";
        _coinsText.text = $"{_coins}";
        _diamondsText.text = $"{_diamonds}";

        _levelUnitText.text = $"{_levelUnit}";
        _priceUnitText.text = $"{_priceUnit}";

        _levelIncomeText.text = $"{_levelIncome}";
        _priceIncomeText.text = $"{_priceIncome}";
        #endregion

    }

    private void BuyIncome()
    {
        //_coins = PlayerPrefs.GetInt("coins");
        if (_coins >= _priceIncome)
        {
            int buy = _coins -= _priceIncome;
            PlayerPrefs.SetInt("coins", buy);
            _coinsText.text = $"{buy}";
            _levelIncome += 1;
            _priceIncome += 100;
            _levelIncomeText.text = $"{_levelIncome}";
            _priceIncomeText.text = $"{_priceIncome}";
            PlayerPrefs.SetInt("levelIncome", _levelIncome);
        }
    }

    public void Income() => BuyIncome();

    private void BuyUnit()
    {
        //_coins = PlayerPrefs.GetInt("coins");
        if (_coins >= _priceUnit)
        {
            int buy = _coins -= _priceUnit;
            PlayerPrefs.SetInt("coins", buy);
            _coinsText.text = $"{buy}";
            _levelUnit += 1;
            _priceUnit += 100;
            _levelUnitText.text = $"{_levelUnit}";
            _priceUnitText.text = $"{_priceUnit}";
            _spawner.UnitSpawn(1);
            PlayerPrefs.SetInt("levelUnit", _levelUnit);
            _unitCounter.text = $"{_levelUnit}";
        }
    }

    public void Unit() => BuyUnit();

}
