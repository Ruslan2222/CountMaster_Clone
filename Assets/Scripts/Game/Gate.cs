using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [Header("Gate")]
    [Space]
    [SerializeField] private BoxCollider _boxColliders;
    [SerializeField] private TextMeshPro _countText;
    [Space]
    [SerializeField] private GateRow _gateRow;

    private int _randomNumbers;
    private bool _isMultiply;

    private void Start()
    {
        GetCount();
    }

    private void GetCount()
    {
        _isMultiply = Random.value > 0.5;

        if (_isMultiply)
        {
            _randomNumbers = Random.Range(1, 3);
            _countText.text = $"X{_randomNumbers}";
        }
        else
        {
            _randomNumbers = Random.Range(10, 80);

            if (_randomNumbers % 2 != 0)
                _randomNumbers += 1;

            _countText.text = $"+{_randomNumbers}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _gateRow.OffColiders();
            player.GetCounts(_isMultiply, _randomNumbers);
        }
    }


}
