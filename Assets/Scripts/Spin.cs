using System.Collections;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private int _countColors;
    [SerializeField] float _timeRotate;
    [SerializeField] private float _numberRotate;
    [SerializeField] private Transform _listData;

    private const float _circle = 360.0f;
    private float _angle;
    private float _currentTime;

    [SerializeField] private AnimationCurve _curve;
    [Space]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _prefabMan;


    private ListColor _listColor;
    private Material _material;
    public Transform[] _character;

    private void Start()
    {
        _angle = _circle / _countColors;
        SetPositionData();
    }

    private void SetPositionData()
    {
        for (int i = 0; i < _listData.childCount; i++)
        {
            _listData.GetChild(i).eulerAngles = new Vector3(0, 0, -_circle / _countColors * 1);
        }
    }

    private IEnumerator RotateWheel()
    {
        float startAngle = transform.eulerAngles.z;
        _currentTime = 0;

        int indexGift = Random.Range(1, _countColors);

        float angleWant = (_numberRotate * _circle) + _angle * indexGift - startAngle;

        while (_currentTime < _timeRotate)
        {
            yield return new WaitForEndOfFrame();
            _currentTime += Time.deltaTime;

            float angleCurrent = angleWant * _curve.Evaluate(_currentTime / _timeRotate);
            transform.eulerAngles = new Vector3(0, 0, angleCurrent + startAngle);
        }

        GetColor(indexGift);
    }

    private void GetColor(int index)
    {
        _character = new Transform[_player.childCount - 1];
        _listColor = _listData.GetChild(index).GetComponent<ListColor>();
        _material = _listColor._unitMaterial;
        _prefabMan.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _material;
        for (int i = 0; i < _player.childCount - 1; i++)
        {
            _character[i] = _player.GetChild(i+1).transform;
            _character[i].GetChild(1).GetComponent<SkinnedMeshRenderer>().material = _material;
        }
    }

    private void RotateNow()
    {
        StartCoroutine(RotateWheel());
    }

    public void SpinWheel() => RotateNow();

}
