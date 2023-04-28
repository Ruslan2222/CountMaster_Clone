using UnityEngine;
using Cinemachine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] private TextMeshPro _counterText;
    [Header("Spawn Settings")]
    [Space]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _prefabMan;
    [Range(0, 1)] [SerializeField] private float _distance, _radius;
    [Header("Camera")]
    [Space]
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _multiplierCamera;
    [Header("Tower")]
    [Space]
    [SerializeField] private Tower _tower;
    [Header("Panels")]
    [Space]
    [SerializeField] private GameObject _winGamePanel;
    public GameObject winPanel => _winGamePanel;

    [SerializeField] private GameObject _losePanel;
    [Space]
    [SerializeField] private SidewayMovement _sidewayMovement;

    public static bool isMoving;
    public bool moveCamera;

    private int _numberOfMans;
    private bool _attackEnemy;
    private Transform _enemy;

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 300);
        GetData();
    }

    private void OnDisable()
    {
        isMoving = false;
    }

    private void GetData()
    {
        int levelUnit = PlayerPrefs.GetInt("levelUnit");
        _spawner.UnitSpawn(levelUnit);
        _numberOfMans = transform.childCount - 1;
        _counterText.text = $"{_numberOfMans}";
    }

    private void Update()
    {

        #region Attack
        if (_attackEnemy)
        {
            ForwardMovement.speed = 0.4f;
            Vector3 enemyDirection = _enemy.position - transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);
            }

            
            if (transform.childCount <= 1)
            {
                Time.timeScale = 0f;
                _losePanel.SetActive(true);
            }
            else if(_enemy.childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = _enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < 1.5f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            new Vector3(_enemy.GetChild(1).GetChild(0).position.x, transform.GetChild(i).position.y,
                                _enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 1f);
                    }
                }


            }
            else
            {
                for (int i = 1; i < transform.childCount; i++)
                    transform.GetChild(i).rotation = Quaternion.identity;
                _attackEnemy = false;
                ForwardMovement.speed = 3f;
                Format();
            }

            _numberOfMans = transform.childCount - 1;
            _counterText.text = $"{_numberOfMans}";
        }
        #endregion

        #region Camera
        if (moveCamera && transform.childCount > 1)
        {
            var cinemachineTransposer = _multiplierCamera.GetCinemachineComponent<CinemachineTransposer>();

            var cinemachineComposer = _multiplierCamera.GetCinemachineComponent<CinemachineComposer>();

            cinemachineTransposer.m_FollowOffset = new Vector3(4.5f, Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y,
                transform.GetChild(1).position.y + 4f, Time.deltaTime * 1f), -5f);

            cinemachineComposer.m_TrackedObjectOffset = new Vector3(0f, Mathf.Lerp(cinemachineComposer.m_TrackedObjectOffset.y,
                5f, Time.deltaTime * 1f), 0f);
        }
        #endregion
    }

    private void Format()
    {
        _spawner.Format(transform); 
    }

    public void FormatCharacher() => Format();

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyContainer enemyContainer))
        {
            _attackEnemy = true;
            _enemy = enemyContainer.transform;
            enemyContainer.AttackPlayer(transform);
        }
        else if (other.tag == "Ramp")
        {
            Invoke("Format", 1.5f);
        }
        else if (other.tag == "FinishLine")
        {
            FinishLine();
        }
    }

    private void FinishLine()
    {
        _sidewayMovement.enabled = false;
        _multiplierCamera.Priority = 15;
        _tower.CreateTower(transform.childCount - 1);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void GetCount(bool isMultiply, int countMans)
    {
        int countPrefab;

        if (isMultiply)
        {
            if (countMans > 1)
            {
                _numberOfMans = transform.childCount - 1;
                countPrefab = countMans * _numberOfMans;
                _spawner.SpawnMan(countPrefab, _prefabMan, transform);
            }
        }
        else
        {
            countPrefab = countMans;
            _spawner.SpawnMan(countPrefab, _prefabMan, transform);
        }

        _counterText.text = $"{transform.childCount - 1}";
    }

    public void GetCounts(bool isMultiply, int countMans) => GetCount(isMultiply, countMans);

    private void CheckCount()
    {
        StartCoroutine(Lose());
    }
    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.4f);
        _counterText.text = $"{transform.childCount - 1}";
        yield return new WaitForSeconds(1f);
        if (transform.childCount <= 1)
        {
            _losePanel.SetActive(true);
        }
        else if(transform.childCount != 1 && !_attackEnemy)
        {
            _spawner.Format(transform);
        }
    }

    public void LoseGame() => CheckCount();
}